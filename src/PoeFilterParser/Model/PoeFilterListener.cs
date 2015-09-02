using Antlr4.Runtime.Misc;

#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace PoeFilterParser.Model
{
    using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;

    /// <summary>
    /// This interface defines a complete listener for a parse tree produced by
    /// <see cref="PoeFilterParser"/>.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5-SNAPSHOT")]
    public interface IPoeFilterListener : IParseTreeListener
    {
        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.strValue"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterStrValue([NotNull] PoeFilterParser.StrValueContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.strValue"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitStrValue([NotNull] PoeFilterParser.StrValueContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.socketValue"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterSocketValue([NotNull] PoeFilterParser.SocketValueContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.socketValue"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitSocketValue([NotNull] PoeFilterParser.SocketValueContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.rarityValue"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterRarityValue([NotNull] PoeFilterParser.RarityValueContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.rarityValue"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitRarityValue([NotNull] PoeFilterParser.RarityValueContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.compareOpNullable"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterCompareOpNullable([NotNull] PoeFilterParser.CompareOpNullableContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.compareOpNullable"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitCompareOpNullable([NotNull] PoeFilterParser.CompareOpNullableContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.visibility"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterVisibility([NotNull] PoeFilterParser.VisibilityContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.visibility"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitVisibility([NotNull] PoeFilterParser.VisibilityContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.params"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterParams([NotNull] PoeFilterParser.ParamsContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.params"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitParams([NotNull] PoeFilterParser.ParamsContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.digitsParams"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterDigitsParams([NotNull] PoeFilterParser.DigitsParamsContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.digitsParams"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitDigitsParams([NotNull] PoeFilterParser.DigitsParamsContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.rariryParams"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterRariryParams([NotNull] PoeFilterParser.RariryParamsContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.rariryParams"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitRariryParams([NotNull] PoeFilterParser.RariryParamsContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.socketParams"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterSocketParams([NotNull] PoeFilterParser.SocketParamsContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.socketParams"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitSocketParams([NotNull] PoeFilterParser.SocketParamsContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.red"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterRed([NotNull] PoeFilterParser.RedContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.red"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitRed([NotNull] PoeFilterParser.RedContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.green"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterGreen([NotNull] PoeFilterParser.GreenContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.green"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitGreen([NotNull] PoeFilterParser.GreenContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.blue"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterBlue([NotNull] PoeFilterParser.BlueContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.blue"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitBlue([NotNull] PoeFilterParser.BlueContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.alpha"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterAlpha([NotNull] PoeFilterParser.AlphaContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.alpha"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitAlpha([NotNull] PoeFilterParser.AlphaContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.color"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterColor([NotNull] PoeFilterParser.ColorContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.color"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitColor([NotNull] PoeFilterParser.ColorContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeClass"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeClass([NotNull] PoeFilterParser.PoeClassContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeClass"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeClass([NotNull] PoeFilterParser.PoeClassContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeBaseType"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeBaseType([NotNull] PoeFilterParser.PoeBaseTypeContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeBaseType"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeBaseType([NotNull] PoeFilterParser.PoeBaseTypeContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeFontSize"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeFontSize([NotNull] PoeFilterParser.PoeFontSizeContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeFontSize"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeFontSize([NotNull] PoeFilterParser.PoeFontSizeContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeTextColor"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeTextColor([NotNull] PoeFilterParser.PoeTextColorContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeTextColor"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeTextColor([NotNull] PoeFilterParser.PoeTextColorContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeBackgroundColor"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeBackgroundColor([NotNull] PoeFilterParser.PoeBackgroundColorContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeBackgroundColor"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeBackgroundColor([NotNull] PoeFilterParser.PoeBackgroundColorContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeBorderColor"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeBorderColor([NotNull] PoeFilterParser.PoeBorderColorContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeBorderColor"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeBorderColor([NotNull] PoeFilterParser.PoeBorderColorContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeAlertSound"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeAlertSound([NotNull] PoeFilterParser.PoeAlertSoundContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeAlertSound"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeAlertSound([NotNull] PoeFilterParser.PoeAlertSoundContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeRarity"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeRarity([NotNull] PoeFilterParser.PoeRarityContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeRarity"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeRarity([NotNull] PoeFilterParser.PoeRarityContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeSocketGroup"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeSocketGroup([NotNull] PoeFilterParser.PoeSocketGroupContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeSocketGroup"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeSocketGroup([NotNull] PoeFilterParser.PoeSocketGroupContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeLinkedSockets"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeLinkedSockets([NotNull] PoeFilterParser.PoeLinkedSocketsContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeLinkedSockets"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeLinkedSockets([NotNull] PoeFilterParser.PoeLinkedSocketsContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeSockets"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeSockets([NotNull] PoeFilterParser.PoeSocketsContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeSockets"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeSockets([NotNull] PoeFilterParser.PoeSocketsContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeItemLevel"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeItemLevel([NotNull] PoeFilterParser.PoeItemLevelContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeItemLevel"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeItemLevel([NotNull] PoeFilterParser.PoeItemLevelContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeDropLevel"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeDropLevel([NotNull] PoeFilterParser.PoeDropLevelContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeDropLevel"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeDropLevel([NotNull] PoeFilterParser.PoeDropLevelContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeQuality"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeQuality([NotNull] PoeFilterParser.PoeQualityContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeQuality"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeQuality([NotNull] PoeFilterParser.PoeQualityContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeWidth"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeWidth([NotNull] PoeFilterParser.PoeWidthContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeWidth"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeWidth([NotNull] PoeFilterParser.PoeWidthContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.poeHeight"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterPoeHeight([NotNull] PoeFilterParser.PoeHeightContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.poeHeight"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitPoeHeight([NotNull] PoeFilterParser.PoeHeightContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.statement"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterStatement([NotNull] PoeFilterParser.StatementContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.statement"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitStatement([NotNull] PoeFilterParser.StatementContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.block"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterBlock([NotNull] PoeFilterParser.BlockContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.block"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitBlock([NotNull] PoeFilterParser.BlockContext context);

        /// <summary>
        /// Enter a parse tree produced by <see cref="PoeFilterParser.main"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void EnterMain([NotNull] PoeFilterParser.MainContext context);

        /// <summary>
        /// Exit a parse tree produced by <see cref="PoeFilterParser.main"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        void ExitMain([NotNull] PoeFilterParser.MainContext context);
    }
} // namespace PoeFilterParser