using FluentAssertions;
using NoStringEvaluating.Factories;
using NUnit.Framework;

namespace NoStringEvaluating.Tests.UnitTests.Factories;

internal class BooleanFactoryTests
{
    [TestCase(true)]
    [TestCase(false)]
    public void Should_Create_A_Nullable_Boolean(bool boolean)
    {
        var factory = default(BooleanFactory);

        var value = factory.Create((bool?)boolean);

        value.IsBoolean.Should().BeTrue();
        value.Boolean.Should().Be(boolean);
    }

    [Test]
    public void Should_Create_A_Null_Value()
    {
        var factory = default(BooleanFactory);

        var value = factory.Create(null);

        value.IsNull.Should().BeTrue();
        value.Boolean.Should().BeFalse();
    }
}
