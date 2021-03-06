﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using AutoStep.Elements.Metadata;
using AutoStep.Execution;
using AutoStep.Execution.Contexts;
using AutoStep.Execution.Control;
using AutoStep.Execution.Dependency;
using AutoStep.Execution.Events;
using AutoStep.Execution.Logging;
using AutoStep.Execution.Strategy;
using AutoStep.Tests.Builders;
using AutoStep.Tests.Utils;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace AutoStep.Tests.Execution.Strategy
{
    public class DefaultStepCollectionExecutionStrategyTests : LoggingTestBase
    {
        public DefaultStepCollectionExecutionStrategyTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async Task RunsAllSteps()
        {
            var stepCollection = new ScenarioBuilder("My Scenario", 1, 1)
                                     .Given("I have done something", 2, 1)
                                     .Then("Something else happens", 3, 1)
                                     .And("a really big thing happens", StepType.Then, 4, 1)
                                     .Built;

            var variables = new VariableSet();

            var owningContext = new ScenarioContext(stepCollection, variables);

            int ranSteps = 0;

            await DoTest(stepCollection, 3, owningContext, variables, (scope, ctxt, varSet) =>
            {
                scope.Tag.Should().Be(ScopeTags.StepTag);
                ctxt.StepIndex.Should().Be(ranSteps);
                ctxt.Step.Should().Be(stepCollection.Steps[ranSteps]);
                ctxt.Variables.Should().Be(variables);
                varSet.Should().Be(variables);
                ctxt.FailException.Should().BeNull();

                ranSteps++;
            });

            ranSteps.Should().Be(3);
        }

        [Fact]
        public async Task StopsTestOnCancellation()
        {
            var stepCollection = new ScenarioBuilder("My Scenario", 1, 1)
                                     .Given("I have done something", 2, 1)
                                     .Then("Something else happens", 3, 1)
                                     .And("a really big thing happens", StepType.Then, 4, 1)
                                     .Built;

            var variables = new VariableSet();

            var owningContext = new ScenarioContext(stepCollection, variables);

            int ranSteps = 0;
            var cancelSource = new CancellationTokenSource();

            await DoTest(stepCollection, 2, owningContext, variables, (scope, ctxt, varSet) =>
            {
                ranSteps++;

                if (ranSteps == 2)
                {
                    cancelSource.Cancel();
                }
            }, cancelSource.Token);

            ranSteps.Should().Be(2);
            owningContext.FailException.Should().BeOfType<OperationCanceledException>();
        }

        [Fact]
        public async Task StopsTestIfStepFails()
        {
            var stepCollection = new ScenarioBuilder("My Scenario", 1, 1)
                                     .Given("I have done something", 2, 1)
                                     .Then("Something else happens", 3, 1)
                                     .And("a really big thing happens", StepType.Then, 4, 1)
                                     .Built;

            var variables = new VariableSet();

            var owningContext = new ScenarioContext(stepCollection, variables);

            int ranSteps = 0;

            await DoTest(stepCollection, 2, owningContext, variables, (scope, ctxt, varSet) =>
            {
                scope.Tag.Should().Be(ScopeTags.StepTag);
                ctxt.StepIndex.Should().Be(ranSteps);
                ctxt.Step.Should().Be(stepCollection.Steps[ranSteps]);
                ctxt.Variables.Should().Be(variables);
                varSet.Should().Be(variables);

                ranSteps++;

                if (ctxt.Step.Type == StepType.Then)
                {
                    // Throw.
                    throw new NullReferenceException();
                }
            });

            ranSteps.Should().Be(2);

            owningContext.FailException.Should().NotBeNull();
            owningContext.FailException.Should().BeOfType<StepFailureException>();
            owningContext.FailException!.InnerException.Should().BeOfType<NullReferenceException>();
            owningContext.FailingStep.Should().Be(stepCollection.Steps[1]);
        }

        [Fact]
        public async Task StopsTestIfEventHandlerFails()
        {
            var stepCollection = new ScenarioBuilder("My Scenario", 1, 1)
                                     .Given("I have done something", 2, 1)
                                     .Then("Something else happens", 3, 1)
                                     .And("a really big thing happens", StepType.Then, 4, 1)
                                     .Built;

            var variables = new VariableSet();

            var owningContext = new ScenarioContext(stepCollection, variables);

            int ranSteps = 0;

            var beforeFeat = 0;
            var afterFeat = 0;
            var eventHandler = new MyEventHandler(ctxt =>
            {
                beforeFeat++;

                if (beforeFeat == 2)
                {
                    throw new NullReferenceException();
                }
            }, c => afterFeat++);

            await DoTest(stepCollection, 2, owningContext, variables, eventHandler, (scope, ctxt, varSet) =>
            {
                scope.Tag.Should().Be(ScopeTags.StepTag);
                ctxt.StepIndex.Should().Be(ranSteps);
                ctxt.Step.Should().Be(stepCollection.Steps[ranSteps]);
                ctxt.Variables.Should().Be(variables);
                varSet.Should().Be(variables);

                ranSteps++;
            });

            ranSteps.Should().Be(1);

            beforeFeat.Should().Be(2);
            afterFeat.Should().Be(1);

            owningContext.FailException.Should().NotBeNull();
            owningContext.FailException.Should().BeOfType<EventHandlingException>();
            owningContext.FailException!.InnerException.Should().BeOfType<NullReferenceException>();
            owningContext.FailingStep.Should().Be(stepCollection.Steps[1]);
        }

        private async ValueTask DoTest(
            IStepCollectionInfo stepCollection,
            int executionManagerInvokes,
            StepCollectionContext owningContext,
            VariableSet variables,
            Action<ILifetimeScope, StepContext, VariableSet> stepCallback,
            CancellationToken cancelToken = default)
        {
            var beforeFeat = 0;
            var afterFeat = 0;
            var eventHandler = new MyEventHandler(ctxt =>
            {
                beforeFeat++;
            }, c => afterFeat++);

            await DoTest(stepCollection, executionManagerInvokes, owningContext, variables, eventHandler, stepCallback, cancelToken);
        }

        private async ValueTask DoTest(
            IStepCollectionInfo stepCollection,
            int executionManagerInvokes,
            StepCollectionContext owningContext,
            VariableSet variables,
            IEventHandler eventHandler,
            Action<ILifetimeScope, StepContext, VariableSet> stepCallback,
            CancellationToken cancelToken = default)
        {
            var threadContext = new ThreadContext(1);
            var mockExecutionStateManager = new Mock<IExecutionStateManager>();

            var eventPipeline = new EventPipeline(new List<IEventHandler> { eventHandler });
            var stepExecuteStrategy = new MyStepExecutionStrategy(stepCallback);

            var contextProvider = new Mock<IContextScopeProvider>();
            var allTrackedDisposables = new List<TestDisposable>();

            contextProvider.Setup(x => x.EnterContextScope(It.IsAny<TestExecutionContext>())).Returns<TestExecutionContext>(ctxt =>
            {
                ctxt.Should().BeOfType<StepContext>();

                var newDisposable = new TestDisposable();
                allTrackedDisposables.Add(newDisposable);
                return newDisposable;
            });

            var builder = new ContainerBuilder();
            builder.RegisterInstance(LogFactory);
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));
            builder.RegisterInstance(mockExecutionStateManager.Object);
            builder.RegisterInstance<IEventPipeline>(eventPipeline);
            builder.RegisterInstance(contextProvider.Object);
            builder.RegisterInstance(threadContext);
            builder.RegisterInstance<IStepExecutionStrategy>(stepExecuteStrategy);
            builder.RegisterInstance(contextProvider.Object);

            var scope = builder.Build();

            var strategy = new DefaultStepCollectionExecutionStrategy();

            await strategy.ExecuteAsync(scope, owningContext, stepCollection, variables, cancelToken);

            mockExecutionStateManager.Verify(x => x.CheckforHalt(It.IsAny<ILifetimeScope>(), It.IsAny<StepContext>(), TestThreadState.StartingStep),
                                             Times.Exactly(executionManagerInvokes));

            if (cancelToken == default)
            {
                allTrackedDisposables.Should().HaveCount(executionManagerInvokes);
            }
            else
            {
                // Allow the number of disposables to be 1 greater than the manager invokes because of cancellation support.
                allTrackedDisposables.Should().HaveCount(executionManagerInvokes + 1);
            }

            allTrackedDisposables.TrueForAll(d => d.IsDisposed).Should().BeTrue();

            if(owningContext.FailException != null)
            {
                mockExecutionStateManager.Verify(x => x.StepError(It.IsAny<StepContext>()), Times.Once());
            }
        }

        private class MyStepExecutionStrategy : IStepExecutionStrategy
        {
            private readonly Action<ILifetimeScope, StepContext, VariableSet> stepCallback;

            public MyStepExecutionStrategy(Action<ILifetimeScope, StepContext, VariableSet> stepCallback)
            {
                this.stepCallback = stepCallback;
            }

            public ValueTask ExecuteStepAsync(ILifetimeScope stepScope, StepContext context, VariableSet variables, CancellationToken cancelToken)
            {
                stepScope.Should().NotBeNull();
                stepScope.Tag.Should().Be(ScopeTags.StepTag);

                stepCallback(stepScope, context, variables);

                return default;
            }
        }

        private class MyEventHandler : BaseEventHandler
        {
            private readonly Action<StepContext> callBefore;
            private readonly Action<StepContext> callAfter;
            private readonly Action<Exception>? exception;

            public MyEventHandler(Action<StepContext> callBefore, Action<StepContext> callAfter, Action<Exception>? exception = null)
            {
                this.callBefore = callBefore;
                this.callAfter = callAfter;
                this.exception = exception;
            }

            public MyEventHandler(Action<Exception> exception)
            {
                this.callBefore = c => { };
                this.callAfter = c => { };
                this.exception = exception;
            }

            public override async ValueTask OnStepAsync(ILifetimeScope scope, StepContext ctxt, Func<ILifetimeScope, StepContext, CancellationToken, ValueTask> next, CancellationToken cancelToken)
            {
                callBefore(ctxt);

                try
                {
                    await next(scope, ctxt, cancelToken);
                }
                catch (Exception ex)
                {
                    if (exception is object)
                    {
                        exception(ex);
                    }
                    else
                    {
                        throw;
                    }
                }

                callAfter(ctxt);
            }
        }
    }
}
