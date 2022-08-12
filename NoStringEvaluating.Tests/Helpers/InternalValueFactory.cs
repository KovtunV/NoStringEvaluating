using System.Collections.Generic;
using System.Linq;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluatingTests.Helpers;

internal class InternalValueFactory
{
    private readonly ExtraTypeIdContainer _extraTypeIdContainer = new();

    public InternalValueFactory()
    {
        ValueFactory = new(_extraTypeIdContainer);
    }

    public ValueFactory ValueFactory { get; }

    public List<InternalEvaluatorValue> Create(params EvaluatorValue[] externalValues)
    {
        return externalValues.Select(x => ValueFactory.Create(x)).ToList();
    }
}
