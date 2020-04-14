﻿using Antlr4.Runtime;
using AutoStep.Elements.Interaction;
using AutoStep.Language.Interaction.Parser;
using AutoStep.Language.Position;

namespace AutoStep.Language.Interaction.Visitors
{
    /// <summary>
    /// Provides the visitor for a top-level interactions file.
    /// </summary>
    internal class InteractionsFileVisitor : BaseAutoStepInteractionVisitor<InteractionFileElement>
    {
        private TraitDefinitionVisitor traitVisitor;
        private ComponentDefinitionVisitor componentVisitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionsFileVisitor"/> class.
        /// </summary>
        /// <param name="sourceName">The name of the source.</param>
        /// <param name="tokenStream">The full token stream.</param>
        /// <param name="compilerOptions">The compiler options.</param>
        /// <param name="positionIndex">The position index (or null if not in use).</param>
        public InteractionsFileVisitor(string? sourceName, ITokenStream tokenStream, InteractionsCompilerOptions compilerOptions, PositionIndex? positionIndex)
            : base(sourceName, tokenStream, compilerOptions, positionIndex)
        {
            traitVisitor = new TraitDefinitionVisitor(sourceName, tokenStream, Rewriter, compilerOptions, positionIndex);
            componentVisitor = new ComponentDefinitionVisitor(sourceName, tokenStream, Rewriter, compilerOptions, positionIndex);
        }

        /// <inheritdoc/>
        public override InteractionFileElement VisitFile(AutoStepInteractionsParser.FileContext context)
        {
            Result = new InteractionFileElement
            {
                SourceName = SourceName,
            };

            PositionIndex?.PushScope(Result, context);

            VisitChildren(context);

            PositionIndex?.PopScope(context);

            return Result;
        }

        /// <inheritdoc/>
        public override InteractionFileElement VisitTraitDefinition(AutoStepInteractionsParser.TraitDefinitionContext context)
        {
            var trait = traitVisitor.Build(context);

            MergeVisitorAndReset(traitVisitor);

            if (trait is object)
            {
                // Capture the trait in the graph.
                Result!.TraitGraph.AddOrExtendTrait(trait);
            }

            return Result!;
        }

        /// <inheritdoc/>
        public override InteractionFileElement VisitComponentDefinition(AutoStepInteractionsParser.ComponentDefinitionContext context)
        {
            var component = componentVisitor.Build(context);

            MergeVisitorAndReset(componentVisitor);

            if (component is object)
            {
                Result!.Components.Add(component);
            }

            return base.VisitComponentDefinition(context);
        }
    }
}
