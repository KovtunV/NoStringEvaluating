﻿using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Cosec;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Tests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions.Math.Trigonometry.Cosec;

internal class CosecFunctionTests : FunctionTests<CosecFunction>
{
    [TestCase(18, -1.332)]
    public void Should_Cosec(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
