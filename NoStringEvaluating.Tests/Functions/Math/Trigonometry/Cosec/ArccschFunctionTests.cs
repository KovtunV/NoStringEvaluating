﻿using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Cosec;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Math.Trigonometry.Cosec;

internal class ArccschFunctionTests : FunctionTests<ArccschFunction>
{
    [TestCase(18, 0.056)]
    public void Should_Arccsch(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}