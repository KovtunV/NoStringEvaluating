﻿using System.Linq;
using FluentAssertions;
using NoStringEvaluating.Functions.Math;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions.Math;

internal class MinFunctionTests : FunctionTests<MinFunction>
{
    [Test]
    public void Should_Min()
    {
        // arrange
        var numberList = new[] { 1d, 2, 3, 6 }.ToList();
        var number = 5;
        var expected = 1;

        // act
        var actual = Execute(numberList, number);

        // assert
        actual.TypeKey.Should().Be(ValueTypeKey.Number);
        actual.Number.Should().BeApproximatelyNumber(expected);
    }
}
