using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace PoeFilterParser.Model
{
    /// <summary>
    /// This class provides an empty implementation of <see cref="IPoeFilterVisitor{Result}"/>,
    /// which can be extended to create a visitor which only needs to handle a subset
    /// of the available methods.
    /// </summary>
    /// <typeparam name="Result">The return type of the visit operation.</typeparam>
    [System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5-SNAPSHOT")]
    public partial class PoeFilterBaseVisitor<Result> : AbstractParseTreeVisitor<Result>, IPoeFilterVisitor<Result>
    {
        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.strValue"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitStrValue([NotNull] PoeFilterParser.StrValueContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.socketValue"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitSocketValue([NotNull] PoeFilterParser.SocketValueContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.rarityValue"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitRarityValue([NotNull] PoeFilterParser.RarityValueContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.compareOpNullable"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitCompareOpNullable([NotNull] PoeFilterParser.CompareOpNullableContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.visibility"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitVisibility([NotNull] PoeFilterParser.VisibilityContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.@params"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitParams([NotNull] PoeFilterParser.ParamsContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.digitsParams"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitDigitsParams([NotNull] PoeFilterParser.DigitsParamsContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.rariryParams"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitRariryParams([NotNull] PoeFilterParser.RariryParamsContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.socketParams"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitSocketParams([NotNull] PoeFilterParser.SocketParamsContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.red"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitRed([NotNull] PoeFilterParser.RedContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.green"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitGreen([NotNull] PoeFilterParser.GreenContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.blue"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitBlue([NotNull] PoeFilterParser.BlueContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.alpha"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitAlpha([NotNull] PoeFilterParser.AlphaContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.color"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitColor([NotNull] PoeFilterParser.ColorContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeClass"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeClass([NotNull] PoeFilterParser.PoeClassContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeBaseType"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeBaseType([NotNull] PoeFilterParser.PoeBaseTypeContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeFontSize"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeFontSize([NotNull] PoeFilterParser.PoeFontSizeContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeTextColor"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeTextColor([NotNull] PoeFilterParser.PoeTextColorContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeBackgroundColor"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeBackgroundColor([NotNull] PoeFilterParser.PoeBackgroundColorContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeBorderColor"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeBorderColor([NotNull] PoeFilterParser.PoeBorderColorContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeAlertSound"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeAlertSound([NotNull] PoeFilterParser.PoeAlertSoundContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeRarity"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeRarity([NotNull] PoeFilterParser.PoeRarityContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeSocketGroup"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeSocketGroup([NotNull] PoeFilterParser.PoeSocketGroupContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeLinkedSockets"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeLinkedSockets([NotNull] PoeFilterParser.PoeLinkedSocketsContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeSockets"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeSockets([NotNull] PoeFilterParser.PoeSocketsContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeItemLevel"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeItemLevel([NotNull] PoeFilterParser.PoeItemLevelContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeDropLevel"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeDropLevel([NotNull] PoeFilterParser.PoeDropLevelContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeQuality"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeQuality([NotNull] PoeFilterParser.PoeQualityContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeWidth"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeWidth([NotNull] PoeFilterParser.PoeWidthContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.poeHeight"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitPoeHeight([NotNull] PoeFilterParser.PoeHeightContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.statement"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitStatement([NotNull] PoeFilterParser.StatementContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.block"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitBlock([NotNull] PoeFilterParser.BlockContext context) { return VisitChildren(context); }

        /// <summary>
        /// Visit a parse tree produced by <see cref="PoeFilterParser.main"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual Result VisitMain([NotNull] PoeFilterParser.MainContext context) { return VisitChildren(context); }
    }
} // namespace PoeFilterParser