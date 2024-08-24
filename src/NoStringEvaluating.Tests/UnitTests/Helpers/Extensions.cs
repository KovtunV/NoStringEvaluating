using FluentAssertions;
using FluentAssertions.Numeric;

namespace NoStringEvaluating.Tests.UnitTests.Helpers;

internal static class Extensions
{
    public static AndConstraint<NumericAssertions<double>> BeApproximatelyNumber(
        this NumericAssertions<double> parent,
        double expectedValue)
    {
        if (double.IsNaN(expectedValue))
        {
            return parent.Be(expectedValue);
        }

        return parent.BeApproximately(expectedValue, 0.001);
    }
}
