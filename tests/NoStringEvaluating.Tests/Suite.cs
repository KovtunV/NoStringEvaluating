using FluentAssertions;
using NUnit.Framework;

namespace NoStringEvaluating.Tests;

[SetUpFixture]
public class Suite
{
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        AssertionOptions.AssertEquivalencyUsing(o => o.WithStrictOrdering());
    }
}