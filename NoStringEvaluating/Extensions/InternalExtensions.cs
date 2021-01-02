namespace NoStringEvaluating.Extensions
{
    internal static class InternalExtensions
    {
        internal static bool IsDigitOrPoint(this char ch)
        {
            return ch.IsDigit() || ch == '.';
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
