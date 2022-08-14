using System.Globalization;

namespace NoStringEvaluating.Extensions;

internal static class NumberExtensions
{
    private static readonly string PreventScientificFormat = $"0.{new string('#', 339)}";

    private static readonly double DecimalMin = (double)decimal.MinValue;

    private static readonly double DecimalMax = (double)decimal.MaxValue;

    public static string ToNonScientificString(this double value)
    {
        // prevent scientific notation.  https://stackoverflow.com/questions/1546113/double-to-string-conversion-without-scientific-notation/49663470#49663470
        // we cast to decimal if the value is in the right range (fastest method) and otherwise use the long format method
        return (value > DecimalMin && value < DecimalMax)
            ? ((decimal)value).ToString(CultureInfo.InvariantCulture)
            : value.ToString(PreventScientificFormat, CultureInfo.InvariantCulture);
    }
}
