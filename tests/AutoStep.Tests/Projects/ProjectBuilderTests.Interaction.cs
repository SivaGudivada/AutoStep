﻿using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoStep.Language;
using AutoStep.Definitions;
using AutoStep.Projects;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using AutoStep.Language.Test;
using AutoStep.Language.Interaction;
using AutoStep.Elements.Interaction;
using System.Collections.Generic;

namespace AutoStep.Tests.Projects
{
    public partial class ProjectBuilderTests
    {
        [Fact]
        public async Task CompilesNewInteractionFile()
        {
            var project = new Project();
            var mockSource = new Mock<IContentSource>();
            mockSource.Setup(s => s.GetLastContentModifyTime()).Returns(DateTime.Today);

            var mockInteractionCompiler = new Mock<IInteractionCompiler>();
            // Compilation will return a compilation result (with an empty file).
            mockInteractionCompiler.Setup(x => x.CompileInteractionsAsync(mockSource.Object, It.IsAny<ILoggerFactory>(), default)).Returns(new ValueTask<InteractionsFileCompilerResult>(
                new InteractionsFileCompilerResult(true, Enumerable.Empty<LanguageOperationMessage>(), new InteractionFileElement())
            ));

            var projFile = new ProjectInteractionFile("/file1", mockSource.Object);
            project.TryAddFile(projFile);

            var projectBuilder = GetProjectBuilder(project, mockInteractionCompiler.Object);

            var result = await projectBuilder.CompileAsync();

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();

            projFile.LastCompileResult.Should().NotBeNull();
        }

        [Fact]
        public async Task CompilesMultipleInteractionFilesWithErrors()
        {
            var project = new Project();
            var mockSource1 = new Mock<IContentSource>();
            mockSource1.SetupGet(x => x.SourceName).Returns("/file1");
            mockSource1.Setup(s => s.GetLastContentModifyTime()).Returns(DateTime.Today);

            var mockSource2 = new Mock<IContentSource>();
            mockSource1.SetupGet(x => x.SourceName).Returns("/file2");
            mockSource2.Setup(s => s.GetLastContentModifyTime()).Returns(DateTime.Today);

            var mockInteractionsCompiler = new Mock<IInteractionCompiler>();

            // First compile result
            mockInteractionsCompiler.Setup(x => x.CompileInteractionsAsync(mockSource1.Object, It.IsAny<ILoggerFactory>(), default)).Returns(new ValueTask<InteractionsFileCompilerResult>(
                new InteractionsFileCompilerResult(true, new[] { new LanguageOperationMessage("/file1", CompilerMessageLevel.Error, CompilerMessageCode.SyntaxError, "") })
            ));

            // Second compile result
            mockInteractionsCompiler.Setup(x => x.CompileInteractionsAsync(mockSource2.Object, It.IsAny<ILoggerFactory>(), default)).Returns(new ValueTask<InteractionsFileCompilerResult>(
               new InteractionsFileCompilerResult(true, new[] { new LanguageOperationMessage("/file2", CompilerMessageLevel.Error, CompilerMessageCode.SyntaxError, "") })
            ));

            var projFile1 = new ProjectInteractionFile("/file1", mockSource1.Object);
            var projFile2 = new ProjectInteractionFile("/file2", mockSource2.Object);
            project.TryAddFile(projFile1);
            project.TryAddFile(projFile2);

            var projectBuilder = GetProjectBuilder(project, mockInteractionsCompiler.Object);

            var result = await projectBuilder.CompileAsync();

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();

            var msgs = result.Messages.ToList();

            msgs.Should().HaveCount(2);

            msgs[0].SourceName.Should().Be("/file1");
            msgs[1].SourceName.Should().Be("/file2");
        }

        [Fact]
        public async Task DoesNotCompileInteractionsFileThatHasNotBeenChanged()
        {
            var project = new Project();
            var mockSource = new Mock<IContentSource>();
            mockSource.Setup(s => s.GetLastContentModifyTime()).Returns(DateTime.Today);

            var mockInteractionCompiler = new Mock<IInteractionCompiler>();
            // Compilation will return a compilation result (with an empty file).
            mockInteractionCompiler.Setup(x => x.CompileInteractionsAsync(mockSource.Object, It.IsAny<ILoggerFactory>(), default)).Returns(new ValueTask<InteractionsFileCompilerResult>(
                new InteractionsFileCompilerResult(true, Enumerable.Empty<LanguageOperationMessage>(), new InteractionFileElement())
            ));

            var projFile = new ProjectInteractionFile("/file1", mockSource.Object);
            project.TryAddFile(projFile);

            var projectBuilder = GetProjectBuilder(project, mockInteractionCompiler.Object);

            // Compile once.
            await projectBuilder.CompileAsync();

            var originalCompilationResult = projFile.LastCompileResult;

            // Just do it again.
            await projectBuilder.CompileAsync();

            // Result should be the same.
            projFile.LastCompileResult.Should().BeSameAs(originalCompilationResult);
        }

        [Fact]
        public async Task ReCompilesInteractionFileThatHasBeenChanged()
        {
            var project = new Project();
            var mockSource = new Mock<IContentSource>();

            var changeTime = DateTime.Today;

            mockSource.Setup(s => s.GetLastContentModifyTime()).Returns(() => changeTime);

            var mockInteractionCompiler = new Mock<IInteractionCompiler>();
            // Compilation will return a compilation result (with an empty file).
            mockInteractionCompiler.Setup(x => x.CompileInteractionsAsync(mockSource.Object, It.IsAny<ILoggerFactory>(), default)).Returns(() => new ValueTask<InteractionsFileCompilerResult>(
                new InteractionsFileCompilerResult(true, Enumerable.Empty<LanguageOperationMessage>(), new InteractionFileElement())
            ));

            var projFile = new ProjectInteractionFile("/file1", mockSource.Object);
            project.TryAddFile(projFile);

            var projectBuilder = GetProjectBuilder(project, mockInteractionCompiler.Object);

            // Compile once.
            await projectBuilder.CompileAsync();

            var originalCompilationResult = projFile.LastCompileResult;

            // Change the file timestamp.
            changeTime = projFile.LastCompileTime.AddMinutes(1);

            // Run it again.
            await projectBuilder.CompileAsync();

            // Result should have changed.
            projFile.LastCompileResult.Should().NotBeSameAs(originalCompilationResult);
        }

        [Fact]
        public async Task AddsInteractionCompilationMessagesToProjectMessages()
        {
            var project = new Project();
            var mockSource = new Mock<IContentSource>();
            mockSource.Setup(s => s.GetLastContentModifyTime()).Returns(DateTime.Today);

            var fileMessage = new LanguageOperationMessage("/path", CompilerMessageLevel.Error, CompilerMessageCode.SyntaxError, "");

            var mockInteractionCompiler = new Mock<IInteractionCompiler>();
            // Compilation will return a compilation result (with an empty file).
            mockInteractionCompiler.Setup(x => x.CompileInteractionsAsync(mockSource.Object, It.IsAny<ILoggerFactory>(), default)).Returns(new ValueTask<InteractionsFileCompilerResult>(
                new InteractionsFileCompilerResult(true, new[] { fileMessage }, new InteractionFileElement())
            ));

            var projFile = new ProjectInteractionFile("/path", mockSource.Object);
            project.TryAddFile(projFile);

            var projectBuilder = GetProjectBuilder(project, mockInteractionCompiler.Object);

            // Compile once.
            var overallResult = await projectBuilder.CompileAsync();

            overallResult.Messages.Should().Contain(fileMessage);
        }

        [Fact]
        public async Task AddsIOExceptionCompilerMessageIfIOExceptionThrownInInteractionCompiler()
        {
            var project = new Project();
            var mockSource = new Mock<IContentSource>();
            mockSource.Setup(s => s.SourceName).Returns("/file1");
            mockSource.Setup(s => s.GetLastContentModifyTime()).Returns(DateTime.Today);

            var mockCompiler = new Mock<IInteractionCompiler>();
            mockCompiler.Setup(x => x.CompileInteractionsAsync(mockSource.Object, It.IsAny<ILoggerFactory>(), default)).Throws(new IOException("IO Error"));

            var projFile = new ProjectInteractionFile("/file1", mockSource.Object);
            project.TryAddFile(projFile);

            var projectBuilder = GetProjectBuilder(project, mockCompiler.Object);

            // Compile once.
            var overallResult = await projectBuilder.CompileAsync();

            var expectedMessage = new LanguageOperationMessage("/file1", CompilerMessageLevel.Error, CompilerMessageCode.IOException,
                                                      "File access error: IO Error", 0, 0);

            overallResult.Messages.Should().Contain(expectedMessage);
        }

        [Fact]
        public async Task RebuildingInteractionSetAfterErrorResetsFileMessages()
        {
            var project = new Project();
            var mockSource = new Mock<IContentSource>();
            mockSource.Setup(s => s.SourceName).Returns("/file1");
            mockSource.Setup(s => s.GetLastContentModifyTime()).Returns(DateTime.Today);

            var mockCompiler = new Mock<IInteractionCompiler>();
            mockCompiler.Setup(x => x.CompileInteractionsAsync(mockSource.Object, It.IsAny<ILoggerFactory>(), default)).Returns(new ValueTask<InteractionsFileCompilerResult>(
                new InteractionsFileCompilerResult(true, Enumerable.Empty<LanguageOperationMessage>(), new InteractionFileElement())
            ));

            var projFile = new ProjectInteractionFile("/file1", mockSource.Object);
            project.TryAddFile(projFile);

            var interactionSetBuilder = new Mock<IInteractionSetBuilder>();

            var resultSet = new InteractionSetBuilderResult(false, new[] {
                new LanguageOperationMessage("/file1", CompilerMessageLevel.Error, CompilerMessageCode.InteractionInvalidContent, "")
            }, null);

            interactionSetBuilder.Setup(x => x.Build(It.IsAny<IInteractionsConfiguration>(), true)).Returns(() => resultSet);

            var projectBuilder = GetProjectBuilder(project, mockCompiler.Object, interactionSetBuilder.Object);

            // Compile once.
            await projectBuilder.CompileAsync();

            var expectedMessage = new LanguageOperationMessage("/file1", CompilerMessageLevel.Error, CompilerMessageCode.InteractionInvalidContent,
                                                               "", 0, 0);

            projFile.LastSetBuildResult!.Messages.Should().Contain(expectedMessage);

            resultSet = new InteractionSetBuilderResult(true, Enumerable.Empty<LanguageOperationMessage>(), null);

            mockSource.Setup(s => s.GetLastContentModifyTime()).Returns(DateTime.UtcNow.AddHours(1));

            // Compile once.
            await projectBuilder.CompileAsync();

            projFile.LastSetBuildResult!.Messages.Should().BeEmpty();
        }

        [Fact]
        public async Task AddsUncategorisedExceptionCompilerMessageIfGeneralExceptionThrownInInteractionCompiler()
        {
            var project = new Project();
            var mockSource = new Mock<IContentSource>();
            mockSource.Setup(s => s.SourceName).Returns("/file1");
            mockSource.Setup(s => s.GetLastContentModifyTime()).Returns(DateTime.Today);

            var mockCompiler = new Mock<IInteractionCompiler>();
            mockCompiler.Setup(x => x.CompileInteractionsAsync(mockSource.Object, It.IsAny<ILoggerFactory>(), default)).Throws(new ApplicationException("Unknown Error"));

            var projFile = new ProjectInteractionFile("/file1", mockSource.Object);
            project.TryAddFile(projFile);

            var projectBuilder = GetProjectBuilder(project, mockCompiler.Object);

            // Compile once.
            var overallResult = await projectBuilder.CompileAsync();

            var expectedMessage = new LanguageOperationMessage("/file1", CompilerMessageLevel.Error, CompilerMessageCode.UncategorisedException,
                                                      "Internal Error: Unknown Error", 0, 0);

            overallResult.Messages.Should().Contain(expectedMessage);
        }

        [Fact]
        public void InteractionCompileCanBeCancelled()
        {
            var project = new Project();

            var projectBuilder = GetProjectBuilder(project, new Mock<IInteractionCompiler>().Object);

            var projFile = new ProjectTestFile("/file1", new Mock<IContentSource>().Object);
            project.TryAddFile(projFile);

            var cancelledToken = new CancellationToken(true);

            // Compile once.
            projectBuilder.Invoking(c => c.CompileAsync(cancelledToken)).Should().Throw<OperationCanceledException>();
        }

        private ProjectBuilder GetProjectBuilder(Project project, IInteractionCompiler interactionCompiler, IInteractionSetBuilder setBuilder)
        {
            return new ProjectBuilder(project, new Mock<ITestCompiler>().Object, new Mock<ILinker>().Object, interactionCompiler, () => setBuilder, true);
        }

        private ProjectBuilder GetProjectBuilder(Project project, IInteractionCompiler interactionCompiler)
        {
            var dummySetBuilder = new Mock<IInteractionSetBuilder>();

            dummySetBuilder.Setup(x => x.Build(It.IsAny<IInteractionsConfiguration>(), true))
                           .Returns(new InteractionSetBuilderResult(true, Enumerable.Empty<LanguageOperationMessage>(), new EmptyInteractionSet()));

            return new ProjectBuilder(project, new Mock<ITestCompiler>().Object, new Mock<ILinker>().Object, interactionCompiler, () => dummySetBuilder.Object, true);
        }

        private class EmptyInteractionSet : IInteractionSet
        {
            public IReadOnlyDictionary<string, BuiltComponent> Components => new Dictionary<string, BuiltComponent>();

            public InteractionConstantSet Constants => new InteractionConstantSet();

            public IExtendedMethodTableReferences? ExtendedMethodReferences => null;

            public IEnumerable<StepDefinition> GetStepDefinitions(IStepDefinitionSource stepSource)
            {
                return Enumerable.Empty<StepDefinition>();
            }
        }
    }
}
