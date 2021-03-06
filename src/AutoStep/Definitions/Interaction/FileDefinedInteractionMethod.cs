﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using AutoStep.Elements.Interaction;
using AutoStep.Execution;
using AutoStep.Execution.Contexts;
using AutoStep.Execution.Interaction;
using AutoStep.Language.Interaction;

namespace AutoStep.Definitions.Interaction
{
    /// <summary>
    /// Represents an interaction method defined inside an interaction file.
    /// </summary>
    public class FileDefinedInteractionMethod : InteractionMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileDefinedInteractionMethod"/> class.
        /// </summary>
        /// <param name="name">The method name.</param>
        /// <param name="definitionElement">The method definition element.</param>
        public FileDefinedInteractionMethod(string name, MethodDefinitionElement definitionElement)
            : base(name)
        {
            MethodDefinition = definitionElement;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the method needs defining (i.e. marked with 'needs-defining').
        /// </summary>
        public bool NeedsDefining { get; set; }

        /// <summary>
        /// Gets the method definition element.
        /// </summary>
        public MethodDefinitionElement MethodDefinition { get; }

        /// <summary>
        /// Gets or sets the overriden method that this method definition replaces.
        /// </summary>
        public InteractionMethod? OverriddenMethod { get; set; }

        /// <inheritdoc/>
        public override int ArgumentCount => MethodDefinition.Arguments.Count;

        /// <inheritdoc/>
        public override string? GetDocumentation()
        {
            return MethodDefinition.Documentation;
        }

        /// <inheritdoc/>
        public override async ValueTask InvokeAsync(ILifetimeScope scope, MethodContext context, MethodTable methods, Stack<MethodContext> callStack, CancellationToken cancelToken)
        {
            scope = scope.ThrowIfNull(nameof(scope));
            context = context.ThrowIfNull(nameof(context));
            methods = methods.ThrowIfNull(nameof(methods));
            callStack = callStack.ThrowIfNull(nameof(callStack));

            // Need to use a custom method context for the subsequent method chain invocation, so that we can isolate the variables.
            var localContext = new MethodContext(context.MethodCall!, context.MethodDefinition!, new InteractionVariables())
            {
                ChainValue = context.ChainValue,
            };

            BindArguments(localContext, context.Arguments);

            // Invoke the method chain with the new context.
            await MethodDefinition.InvokeChainAsync(scope, localContext, methods, cancelToken, callStack);

            // 'Return' the chain value from the local context.
            context.ChainValue = localContext.ChainValue;
        }

        private void BindArguments(MethodContext context, IReadOnlyList<object?> arguments)
        {
            // Last chance catch for the wrong number of arguments. Compiler should have caught this.
            if (ArgumentCount != arguments.Count)
            {
                throw new LanguageEngineAssertException();
            }

            for (var argIdx = 0; argIdx < arguments.Count; argIdx++)
            {
                var methodArg = MethodDefinition.Arguments[argIdx];

                context.Variables.Set(methodArg.Name, arguments[argIdx]);
            }
        }
    }
}
