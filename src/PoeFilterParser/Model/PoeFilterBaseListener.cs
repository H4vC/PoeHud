using Antlr4.Runtime.Misc;

#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace PoeFilterParser.Model
{
    using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
    using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
    using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

    /// <summary>
    /// This class provides an empty implementation of <see cref="IPoeFilterListener"/>,
    /// which can be extended to create a listener which only needs to handle a subset
    /// of the available methods.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5-SNAPSHOT")]
    public partial class PoeFilterBaseListener : IPoeFilterListener
    {
        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.strValue"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterStrValue([NotNull] PoeFilterParser.StrValueContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.strValue"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitStrValue([NotNull] PoeFilterParser.StrValueContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.socketValue"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterSocketValue([NotNull] PoeFilterParser.SocketValueContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.socketValue"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitSocketValue([NotNull] PoeFilterParser.SocketValueContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.rarityValue"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterRarityValue([NotNull] PoeFilterParser.RarityValueContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.rarityValue"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitRarityValue([NotNull] PoeFilterParser.RarityValueContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.compareOpNullable"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterCompareOpNullable([NotNull] PoeFilterParser.CompareOpNullableContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.compareOpNullable"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitCompareOpNullable([NotNull] PoeFilterParser.CompareOpNullableContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.visibility"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterVisibility([NotNull] PoeFilterParser.VisibilityContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.visibility"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitVisibility([NotNull] PoeFilterParser.VisibilityContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.@params"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterParams([NotNull] PoeFilterParser.ParamsContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.@params"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitParams([NotNull] PoeFilterParser.ParamsContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.digitsParams"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterDigitsParams([NotNull] PoeFilterParser.DigitsParamsContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.digitsParams"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitDigitsParams([NotNull] PoeFilterParser.DigitsParamsContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.rariryParams"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterRariryParams([NotNull] PoeFilterParser.RariryParamsContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.rariryParams"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitRariryParams([NotNull] PoeFilterParser.RariryParamsContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.socketParams"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterSocketParams([NotNull] PoeFilterParser.SocketParamsContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.socketParams"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitSocketParams([NotNull] PoeFilterParser.SocketParamsContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.red"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterRed([NotNull] PoeFilterParser.RedContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.red"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitRed([NotNull] PoeFilterParser.RedContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.green"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterGreen([NotNull] PoeFilterParser.GreenContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.green"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitGreen([NotNull] PoeFilterParser.GreenContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.blue"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterBlue([NotNull] PoeFilterParser.BlueContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.blue"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitBlue([NotNull] PoeFilterParser.BlueContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.alpha"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterAlpha([NotNull] PoeFilterParser.AlphaContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.alpha"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitAlpha([NotNull] PoeFilterParser.AlphaContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.color"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterColor([NotNull] PoeFilterParser.ColorContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.color"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitColor([NotNull] PoeFilterParser.ColorContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeClass"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeClass([NotNull] PoeFilterParser.PoeClassContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeClass"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeClass([NotNull] PoeFilterParser.PoeClassContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeBaseType"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeBaseType([NotNull] PoeFilterParser.PoeBaseTypeContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeBaseType"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeBaseType([NotNull] PoeFilterParser.PoeBaseTypeContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeFontSize"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeFontSize([NotNull] PoeFilterParser.PoeFontSizeContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeFontSize"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeFontSize([NotNull] PoeFilterParser.PoeFontSizeContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeTextColor"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeTextColor([NotNull] PoeFilterParser.PoeTextColorContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeTextColor"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeTextColor([NotNull] PoeFilterParser.PoeTextColorContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeBackgroundColor"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeBackgroundColor([NotNull] PoeFilterParser.PoeBackgroundColorContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeBackgroundColor"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeBackgroundColor([NotNull] PoeFilterParser.PoeBackgroundColorContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeBorderColor"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeBorderColor([NotNull] PoeFilterParser.PoeBorderColorContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeBorderColor"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeBorderColor([NotNull] PoeFilterParser.PoeBorderColorContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeAlertSound"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeAlertSound([NotNull] PoeFilterParser.PoeAlertSoundContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeAlertSound"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeAlertSound([NotNull] PoeFilterParser.PoeAlertSoundContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeRarity"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeRarity([NotNull] PoeFilterParser.PoeRarityContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeRarity"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeRarity([NotNull] PoeFilterParser.PoeRarityContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeSocketGroup"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeSocketGroup([NotNull] PoeFilterParser.PoeSocketGroupContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeSocketGroup"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeSocketGroup([NotNull] PoeFilterParser.PoeSocketGroupContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeLinkedSockets"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeLinkedSockets([NotNull] PoeFilterParser.PoeLinkedSocketsContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeLinkedSockets"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeLinkedSockets([NotNull] PoeFilterParser.PoeLinkedSocketsContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeSockets"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeSockets([NotNull] PoeFilterParser.PoeSocketsContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeSockets"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeSockets([NotNull] PoeFilterParser.PoeSocketsContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeItemLevel"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeItemLevel([NotNull] PoeFilterParser.PoeItemLevelContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeItemLevel"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeItemLevel([NotNull] PoeFilterParser.PoeItemLevelContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeDropLevel"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeDropLevel([NotNull] PoeFilterParser.PoeDropLevelContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeDropLevel"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeDropLevel([NotNull] PoeFilterParser.PoeDropLevelContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeQuality"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeQuality([NotNull] PoeFilterParser.PoeQualityContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeQuality"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeQuality([NotNull] PoeFilterParser.PoeQualityContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeWidth"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeWidth([NotNull] PoeFilterParser.PoeWidthContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeWidth"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeWidth([NotNull] PoeFilterParser.PoeWidthContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeHeight"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterPoeHeight([NotNull] PoeFilterParser.PoeHeightContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeHeight"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitPoeHeight([NotNull] PoeFilterParser.PoeHeightContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.statement"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterStatement([NotNull] PoeFilterParser.StatementContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.statement"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitStatement([NotNull] PoeFilterParser.StatementContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.block"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterBlock([NotNull] PoeFilterParser.BlockContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.block"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitBlock([NotNull] PoeFilterParser.BlockContext context) { }

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.main"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void EnterMain([NotNull] PoeFilterParser.MainContext context) { }

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.main"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public virtual void ExitMain([NotNull] PoeFilterParser.MainContext context) { }

        /// <inheritdoc/>
        /// <remarks>The default implementation does nothing.</remarks>
        public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }

        /// <inheritdoc/>
        /// <remarks>The default implementation does nothing.</remarks>
        public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }

        /// <inheritdoc/>
        /// <remarks>The default implementation does nothing.</remarks>
        public virtual void VisitTerminal([NotNull] ITerminalNode node) { }

        /// <inheritdoc/>
        /// <remarks>The default implementation does nothing.</remarks>
        public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
    }
} // namespace PoeFilterParser