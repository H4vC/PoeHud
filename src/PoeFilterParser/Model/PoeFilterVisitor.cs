using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace PoeFilterParser.Model {
    /// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="PoeFilterParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5-SNAPSHOT")]

public interface IPoeFilterVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.strValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStrValue([NotNull] PoeFilterParser.StrValueContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.socketValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSocketValue([NotNull] PoeFilterParser.SocketValueContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.rarityValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRarityValue([NotNull] PoeFilterParser.RarityValueContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.compareOpNullable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompareOpNullable([NotNull] PoeFilterParser.CompareOpNullableContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.visibility"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVisibility([NotNull] PoeFilterParser.VisibilityContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.params"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParams([NotNull] PoeFilterParser.ParamsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.digitsParams"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDigitsParams([NotNull] PoeFilterParser.DigitsParamsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.rariryParams"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRariryParams([NotNull] PoeFilterParser.RariryParamsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.socketParams"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSocketParams([NotNull] PoeFilterParser.SocketParamsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.red"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRed([NotNull] PoeFilterParser.RedContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.green"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGreen([NotNull] PoeFilterParser.GreenContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.blue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlue([NotNull] PoeFilterParser.BlueContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.alpha"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAlpha([NotNull] PoeFilterParser.AlphaContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.color"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitColor([NotNull] PoeFilterParser.ColorContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeClass([NotNull] PoeFilterParser.PoeClassContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeBaseType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeBaseType([NotNull] PoeFilterParser.PoeBaseTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeFontSize"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeFontSize([NotNull] PoeFilterParser.PoeFontSizeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeTextColor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeTextColor([NotNull] PoeFilterParser.PoeTextColorContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeBackgroundColor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeBackgroundColor([NotNull] PoeFilterParser.PoeBackgroundColorContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeBorderColor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeBorderColor([NotNull] PoeFilterParser.PoeBorderColorContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeAlertSound"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeAlertSound([NotNull] PoeFilterParser.PoeAlertSoundContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeRarity"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeRarity([NotNull] PoeFilterParser.PoeRarityContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeSocketGroup"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeSocketGroup([NotNull] PoeFilterParser.PoeSocketGroupContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeLinkedSockets"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeLinkedSockets([NotNull] PoeFilterParser.PoeLinkedSocketsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeSockets"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeSockets([NotNull] PoeFilterParser.PoeSocketsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeItemLevel"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeItemLevel([NotNull] PoeFilterParser.PoeItemLevelContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeDropLevel"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeDropLevel([NotNull] PoeFilterParser.PoeDropLevelContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeQuality"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeQuality([NotNull] PoeFilterParser.PoeQualityContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeWidth"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeWidth([NotNull] PoeFilterParser.PoeWidthContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.poeHeight"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPoeHeight([NotNull] PoeFilterParser.PoeHeightContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] PoeFilterParser.StatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] PoeFilterParser.BlockContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PoeFilterParser.main"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMain([NotNull] PoeFilterParser.MainContext context);
}
} // namespace PoeFilterParser
