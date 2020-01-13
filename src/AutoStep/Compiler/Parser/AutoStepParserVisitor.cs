//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from .\AutoStepParser.g4 by ANTLR 4.7.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace AutoStep.Compiler.Parser {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="AutoStepParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.2")]
[System.CLSCompliant(false)]
public interface IAutoStepParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.file"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFile([NotNull] AutoStepParser.FileContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.stepDefinitionBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStepDefinitionBlock([NotNull] AutoStepParser.StepDefinitionBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.stepDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStepDefinition([NotNull] AutoStepParser.StepDefinitionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.stepDefinitionBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStepDefinitionBody([NotNull] AutoStepParser.StepDefinitionBodyContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>declareGiven</c>
	/// labeled alternative in <see cref="AutoStepParser.stepDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclareGiven([NotNull] AutoStepParser.DeclareGivenContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>declareWhen</c>
	/// labeled alternative in <see cref="AutoStepParser.stepDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclareWhen([NotNull] AutoStepParser.DeclareWhenContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>declareThen</c>
	/// labeled alternative in <see cref="AutoStepParser.stepDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclareThen([NotNull] AutoStepParser.DeclareThenContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.stepDeclarationBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStepDeclarationBody([NotNull] AutoStepParser.StepDeclarationBodyContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>declarationArgument</c>
	/// labeled alternative in <see cref="AutoStepParser.stepDeclarationSection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclarationArgument([NotNull] AutoStepParser.DeclarationArgumentContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>declarationSection</c>
	/// labeled alternative in <see cref="AutoStepParser.stepDeclarationSection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclarationSection([NotNull] AutoStepParser.DeclarationSectionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.stepDeclarationArgument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStepDeclarationArgument([NotNull] AutoStepParser.StepDeclarationArgumentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.stepDeclarationArgumentName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStepDeclarationArgumentName([NotNull] AutoStepParser.StepDeclarationArgumentNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.stepDeclarationTypeHint"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStepDeclarationTypeHint([NotNull] AutoStepParser.StepDeclarationTypeHintContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>declarationWord</c>
	/// labeled alternative in <see cref="AutoStepParser.stepDeclarationSectionContent"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclarationWord([NotNull] AutoStepParser.DeclarationWordContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>declarationEscaped</c>
	/// labeled alternative in <see cref="AutoStepParser.stepDeclarationSectionContent"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclarationEscaped([NotNull] AutoStepParser.DeclarationEscapedContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>declarationWs</c>
	/// labeled alternative in <see cref="AutoStepParser.stepDeclarationSectionContent"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclarationWs([NotNull] AutoStepParser.DeclarationWsContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>declarationColon</c>
	/// labeled alternative in <see cref="AutoStepParser.stepDeclarationSectionContent"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclarationColon([NotNull] AutoStepParser.DeclarationColonContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.featureBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFeatureBlock([NotNull] AutoStepParser.FeatureBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.annotations"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnotations([NotNull] AutoStepParser.AnnotationsContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>tagAnnotation</c>
	/// labeled alternative in <see cref="AutoStepParser.annotation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTagAnnotation([NotNull] AutoStepParser.TagAnnotationContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>optionAnnotation</c>
	/// labeled alternative in <see cref="AutoStepParser.annotation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOptionAnnotation([NotNull] AutoStepParser.OptionAnnotationContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>blank</c>
	/// labeled alternative in <see cref="AutoStepParser.annotation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlank([NotNull] AutoStepParser.BlankContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.featureDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFeatureDefinition([NotNull] AutoStepParser.FeatureDefinitionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.featureTitle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFeatureTitle([NotNull] AutoStepParser.FeatureTitleContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.featureBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFeatureBody([NotNull] AutoStepParser.FeatureBodyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.backgroundBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBackgroundBlock([NotNull] AutoStepParser.BackgroundBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.backgroundBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBackgroundBody([NotNull] AutoStepParser.BackgroundBodyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.scenarioBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitScenarioBlock([NotNull] AutoStepParser.ScenarioBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.scenarioDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitScenarioDefinition([NotNull] AutoStepParser.ScenarioDefinitionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>normalScenarioTitle</c>
	/// labeled alternative in <see cref="AutoStepParser.scenarioTitle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNormalScenarioTitle([NotNull] AutoStepParser.NormalScenarioTitleContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>scenarioOutlineTitle</c>
	/// labeled alternative in <see cref="AutoStepParser.scenarioTitle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitScenarioOutlineTitle([NotNull] AutoStepParser.ScenarioOutlineTitleContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.scenarioBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitScenarioBody([NotNull] AutoStepParser.ScenarioBodyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.stepCollectionBodyLine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStepCollectionBodyLine([NotNull] AutoStepParser.StepCollectionBodyLineContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementWithTable</c>
	/// labeled alternative in <see cref="AutoStepParser.statementBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementWithTable([NotNull] AutoStepParser.StatementWithTableContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementLineTerminated</c>
	/// labeled alternative in <see cref="AutoStepParser.statementBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementLineTerminated([NotNull] AutoStepParser.StatementLineTerminatedContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementEofTerminated</c>
	/// labeled alternative in <see cref="AutoStepParser.statementBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementEofTerminated([NotNull] AutoStepParser.StatementEofTerminatedContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>given</c>
	/// labeled alternative in <see cref="AutoStepParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGiven([NotNull] AutoStepParser.GivenContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>when</c>
	/// labeled alternative in <see cref="AutoStepParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhen([NotNull] AutoStepParser.WhenContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>then</c>
	/// labeled alternative in <see cref="AutoStepParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitThen([NotNull] AutoStepParser.ThenContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>and</c>
	/// labeled alternative in <see cref="AutoStepParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnd([NotNull] AutoStepParser.AndContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.statementBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementBody([NotNull] AutoStepParser.StatementBodyContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementQuote</c>
	/// labeled alternative in <see cref="AutoStepParser.statementSection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementQuote([NotNull] AutoStepParser.StatementQuoteContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementDoubleQuote</c>
	/// labeled alternative in <see cref="AutoStepParser.statementSection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementDoubleQuote([NotNull] AutoStepParser.StatementDoubleQuoteContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementVariable</c>
	/// labeled alternative in <see cref="AutoStepParser.statementSection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementVariable([NotNull] AutoStepParser.StatementVariableContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementEscapedChar</c>
	/// labeled alternative in <see cref="AutoStepParser.statementSection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementEscapedChar([NotNull] AutoStepParser.StatementEscapedCharContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementInt</c>
	/// labeled alternative in <see cref="AutoStepParser.statementSection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementInt([NotNull] AutoStepParser.StatementIntContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementFloat</c>
	/// labeled alternative in <see cref="AutoStepParser.statementSection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementFloat([NotNull] AutoStepParser.StatementFloatContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementInterpolate</c>
	/// labeled alternative in <see cref="AutoStepParser.statementSection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementInterpolate([NotNull] AutoStepParser.StatementInterpolateContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementColon</c>
	/// labeled alternative in <see cref="AutoStepParser.statementSection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementColon([NotNull] AutoStepParser.StatementColonContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementWord</c>
	/// labeled alternative in <see cref="AutoStepParser.statementSection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementWord([NotNull] AutoStepParser.StatementWordContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementVarUnmatched</c>
	/// labeled alternative in <see cref="AutoStepParser.statementSection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementVarUnmatched([NotNull] AutoStepParser.StatementVarUnmatchedContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>statementBlockWs</c>
	/// labeled alternative in <see cref="AutoStepParser.statementSection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementBlockWs([NotNull] AutoStepParser.StatementBlockWsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.statementVariableName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementVariableName([NotNull] AutoStepParser.StatementVariableNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.statementVarPhrase"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementVarPhrase([NotNull] AutoStepParser.StatementVarPhraseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.examples"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExamples([NotNull] AutoStepParser.ExamplesContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.exampleBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExampleBlock([NotNull] AutoStepParser.ExampleBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.tableBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTableBlock([NotNull] AutoStepParser.TableBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.tableHeader"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTableHeader([NotNull] AutoStepParser.TableHeaderContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.tableHeaderCell"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTableHeaderCell([NotNull] AutoStepParser.TableHeaderCellContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.tableRow"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTableRow([NotNull] AutoStepParser.TableRowContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.tableRowCell"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTableRowCell([NotNull] AutoStepParser.TableRowCellContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.tableRowCellContent"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTableRowCellContent([NotNull] AutoStepParser.TableRowCellContentContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>cellEscapedChar</c>
	/// labeled alternative in <see cref="AutoStepParser.cellContentBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCellEscapedChar([NotNull] AutoStepParser.CellEscapedCharContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>cellVariable</c>
	/// labeled alternative in <see cref="AutoStepParser.cellContentBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCellVariable([NotNull] AutoStepParser.CellVariableContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>cellInt</c>
	/// labeled alternative in <see cref="AutoStepParser.cellContentBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCellInt([NotNull] AutoStepParser.CellIntContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>cellFloat</c>
	/// labeled alternative in <see cref="AutoStepParser.cellContentBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCellFloat([NotNull] AutoStepParser.CellFloatContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>cellInterpolate</c>
	/// labeled alternative in <see cref="AutoStepParser.cellContentBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCellInterpolate([NotNull] AutoStepParser.CellInterpolateContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>cellColon</c>
	/// labeled alternative in <see cref="AutoStepParser.cellContentBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCellColon([NotNull] AutoStepParser.CellColonContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>cellWord</c>
	/// labeled alternative in <see cref="AutoStepParser.cellContentBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCellWord([NotNull] AutoStepParser.CellWordContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.cellVariableName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCellVariableName([NotNull] AutoStepParser.CellVariableNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.cellVarPhrase"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCellVarPhrase([NotNull] AutoStepParser.CellVarPhraseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.text"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitText([NotNull] AutoStepParser.TextContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLine([NotNull] AutoStepParser.LineContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="AutoStepParser.description"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDescription([NotNull] AutoStepParser.DescriptionContext context);
}
} // namespace AutoStep.Compiler.Parser
