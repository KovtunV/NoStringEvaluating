using NoStringEvaluating.Models;

namespace NoStringEvaluating.Extensions
{
    internal static class InternalExtensions
    {
        internal static bool IsSimpleVariable(this char ch)
        {
            return char.IsLetterOrDigit(ch) || ch == '_';
        }

        internal static bool IsFloatingNumber(this char ch)
        {
            var isDigit = ch.IsDigit();

            return NoStringEvaluatorConstants.FloatingPointSymbol switch
            {
                FloatingPointSymbol.Dot => (isDigit || ch == '.'),
                FloatingPointSymbol.Comma => (isDigit || ch == ','),
                FloatingPointSymbol.DotComma => (isDigit || ch == '.' || ch == ','),
                _ => false
            };
        }

        internal static bool IsDigit(this char ch)
        {
            return char.IsDigit(ch);
        }

        internal static bool IsWhiteSpace(this char? ch)
        {
            return ch.HasValue && char.IsWhiteSpace(ch.Value);
        }

        internal static bool IsWhiteSpace(this char ch)
        {
            return char.IsWhiteSpace(ch);
        }
    }
}
