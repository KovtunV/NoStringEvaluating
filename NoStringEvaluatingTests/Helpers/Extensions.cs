using FluentAssertions;
using FluentAssertions.Numeric;

namespace NoStringEvaluatingTests.Helpers;

internal static class Extensions
{
    public static AndConstraint<NumericAssertions<double>> BeApproximatelyNumber(
        this NumericAssertions<double> parent,
        double expectedValue)
    {
        return parent.BeApproximately(expectedValue, 0.001);
    }
}
