using FluentAssertions;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Functions.Base;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.Functions;

internal class FunctionIntegrationTests
{
    [Test]
    public void Should_Be_Unique_Only_Funcions()
    {
        // arrange, act
        var functions = typeof(IFunction).Assembly.CreateInstances<IFunction>();

        // assert
        functions.Select(x => x.Name).Should().OnlyHaveUniqueItems();
    }
}
