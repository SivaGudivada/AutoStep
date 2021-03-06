﻿using System.Diagnostics;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using AutoStep.Elements.Interaction;
using AutoStep.Elements.Parts;
using AutoStep.Language.Position;

namespace AutoStep.Language.Interaction.Visitors
{
    using static AutoStep.Language.Interaction.Parser.AutoStepInteractionsParser;

    /// <summary>
    /// Handles generating interaction step definitions from the Antlr parse context.
    /// </summary>
    internal class InteractionStepDefinitionVisitor : InteractionCallChainDeclarationVisitor<InteractionStepDefinitionElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionStepDefinitionVisitor"/> class.
        /// </summary>
        /// <param name="sourceName">The name of the source.</param>
        /// <param name="tokenStream">The token stream.</param>
        /// <param name="rewriter">A shared escape rewriter.</param>
        /// <param name="compilerOptions">The compiler options.</param>
        /// <param name="positionIndex">The position index (or null if not in use).</param>
        public InteractionStepDefinitionVisitor(string? sourceName, ITokenStream tokenStream, TokenStreamRewriter rewriter, InteractionsCompilerOptions compilerOptions, PositionIndex? positionIndex)
            : base(sourceName, tokenStream, rewriter, compilerOptions, positionIndex)
        {
        }

        /// <summary>
        /// Builds a step, taking the Step Type, and the Antlr context for the declaration body.
        /// </summary>
        /// <param name="definitionContext">The step declaration context.</param>
        /// <returns>A generated step reference.</returns>
        public InteractionStepDefinitionElement BuildStepDefinition(StepDefinitionBodyContext definitionContext)
        {
            Result = new InteractionStepDefinitionElement
            {
                Description = GetDocumentationBlockForElement(definitionContext),
                SourceName = SourceName,
            };

            PositionIndex?.PushScope(Result, definitionContext);

            var declaration = definitionContext.stepDefinition()?.STEP_DEFINE();

            if (declaration is object)
            {
                PositionIndex?.AddLineToken(declaration, LineTokenCategory.EntryMarker, LineTokenSubCategory.StepDefine);
            }

            VisitChildren(definitionContext);

            PositionIndex?.PopScope(definitionContext);

            return Result;
        }

        /// <inheritdoc/>
        public override InteractionStepDefinitionElement VisitDeclareGiven([NotNull] DeclareGivenContext context)
        {
            Result!.Type = StepType.Given;

            Result.AddPositionalLineInfo(context);

            PositionIndex?.AddLineToken(context.DEF_GIVEN(), LineTokenCategory.StepTypeKeyword, LineTokenSubCategory.Given);

            VisitChildren(context);

            return Result;
        }

        /// <inheritdoc/>
        public override InteractionStepDefinitionElement VisitDeclareWhen([NotNull] DeclareWhenContext context)
        {
            Result!.Type = StepType.When;

            Result.AddPositionalLineInfo(context);

            PositionIndex?.AddLineToken(context.DEF_WHEN(), LineTokenCategory.StepTypeKeyword, LineTokenSubCategory.When);

            VisitChildren(context);

            return Result;
        }

        /// <inheritdoc/>
        public override InteractionStepDefinitionElement VisitDeclareThen([NotNull] DeclareThenContext context)
        {
            Result!.Type = StepType.Then;

            Result.AddPositionalLineInfo(context);

            PositionIndex?.AddLineToken(context.DEF_THEN(), LineTokenCategory.StepTypeKeyword, LineTokenSubCategory.Then);

            VisitChildren(context);

            return Result;
        }

        /// <inheritdoc/>
        public override InteractionStepDefinitionElement VisitStepDeclarationBody([NotNull] StepDeclarationBodyContext context)
        {
            Result!.Declaration = context.GetText();

            VisitChildren(context);

            return Result;
        }

        /// <inheritdoc/>
        public override InteractionStepDefinitionElement VisitDeclarationArgument([NotNull] DeclarationArgumentContext context)
        {
            Debug.Assert(Result is object);

            var content = context.stepDeclarationArgument();

            var name = content.stepDeclarationArgumentName().GetText();
            var hint = DetermineTypeHint(name, content.stepDeclarationTypeHint()?.GetText());

            var part = new ArgumentPart(context.GetText(), name, hint).AddPositionalLineInfo(context);

            AddPart(part);

            return Result;
        }

        private ArgumentType? DetermineTypeHint(string name, string? typeHint)
        {
            if (typeHint == null)
            {
                // No explicit type hint, but we might be able to take it from the name.
                typeHint = name;
            }

            return typeHint switch
            {
                "int" => ArgumentType.NumericInteger,
                "long" => ArgumentType.NumericInteger,
                "float" => ArgumentType.NumericDecimal,
                "double" => ArgumentType.NumericDecimal,
                "decimal" => ArgumentType.NumericDecimal,
                "word" => ArgumentType.Text,
                _ => null
            };
        }

        /// <summary>
        /// Visits a word declaration part.
        /// </summary>
        /// <param name="context">The parser context.</param>
        /// <returns>The step definition.</returns>
        public override InteractionStepDefinitionElement VisitDeclarationWord([NotNull] DeclarationWordContext context)
        {
            AddPart(new WordDefinitionPart(context.GetText()).AddPositionalLineInfo(context));

            return Result!;
        }

        /// <summary>
        /// Visits an escaped character part.
        /// </summary>
        /// <param name="context">The parser context.</param>
        /// <returns>The step definition.</returns>
        public override InteractionStepDefinitionElement VisitDeclarationEscaped(DeclarationEscapedContext context)
        {
            var part = new WordDefinitionPart(context.GetText()).AddPositionalLineInfo(context);

            // TODO
            // part.EscapedText = EscapeText(context, escapeReplacements);
            AddPart(part);

            return Result!;
        }

        /// <summary>
        /// Visits a colon part.
        /// </summary>
        /// <param name="context">The parser context.</param>
        /// <returns>The step definition.</returns>
        public override InteractionStepDefinitionElement VisitDeclarationColon(DeclarationColonContext context)
        {
            AddPart(new WordDefinitionPart(context.GetText()).AddPositionalLineInfo(context));

            return Result!;
        }

        /// <inheritdoc/>
        public override InteractionStepDefinitionElement VisitDeclarationComponentInsert([NotNull] DeclarationComponentInsertContext context)
        {
            AddPart(new PlaceholderMatchPart(StepPlaceholders.Component).AddPositionalLineInfo(context));

            return Result!;
        }

        private void AddPart(DefinitionPart part)
        {
            Result!.AddPart(part);
            PositionIndex?.AddLineToken(part, LineTokenCategory.StepText, LineTokenSubCategory.Declaration);
        }

        private void AddPart(PlaceholderMatchPart part)
        {
            Result!.AddPart(part);
            PositionIndex?.AddLineToken(part, LineTokenCategory.Placeholder, LineTokenSubCategory.InteractionComponentPlaceholder);
        }

        private void AddPart(ArgumentPart part)
        {
            Result!.AddPart(part);
            PositionIndex?.AddLineToken(part, LineTokenCategory.BoundArgument, LineTokenSubCategory.Declaration);
        }
    }
}
