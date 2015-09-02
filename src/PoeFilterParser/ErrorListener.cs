using Antlr4.Runtime;

namespace PoeFilterParser
{
    public class ErrorListener : IAntlrErrorListener<IToken>
    {
        public void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new SyntaxErrorException(line, charPositionInLine, msg);
        }
    }
}