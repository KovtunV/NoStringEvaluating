using System.Globalization;
using AutoFixture;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using NoStringEvaluatingTests.Helpers;
using NUnit.Framework;

namespace NoStringEvaluatingTests.Functions;

internal abstract class FunctionTests<TFunction>
    where TFunction : IFunction, new()
{
    protected CultureInfo _culture;
    protected IFixture _fixture;
    protected InternalValueFactory _valueFactory;
    protected TFunction _function;

    [SetUp]
    public void Setup()
    {
        _culture = CultureInfo.GetCultureInfo("ru-RU");
        _fixture = new Fixture();
        _valueFactory = new();
        _function = new();
    }

    protected InternalEvaluatorValue Execute(params EvaluatorValue[] arguments)
    {
        var functionArguments = _valueFactory.Create(arguments);
        return _function.Execute(functionArguments, _valueFactory.ValueFactory);
    }
}
