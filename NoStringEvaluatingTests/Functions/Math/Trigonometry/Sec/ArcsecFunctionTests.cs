﻿using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Sec;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Math.Trigonometry.Sec;

internal class ArcsecFunctionTests : FunctionTests<ArcsecFunction>
{
    [TestCase(12, 1.487)]
    public void Should_Arcsec(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
