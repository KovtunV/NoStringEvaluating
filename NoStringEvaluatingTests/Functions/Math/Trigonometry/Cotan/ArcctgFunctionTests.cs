﻿using FluentAssertions;
using NoStringEvaluating.Functions.Math.Trigonometry.Cotan;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Math.Trigonometry.Cotan;

internal class ArcctgFunctionTests : FunctionTests<ArcctgFunction>
{
    [TestCase(0.8, 0.896)]
    public void Should_Arcctg(double value, double expected)
    {
        // arrange, act
        var actual = Execute(value);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
