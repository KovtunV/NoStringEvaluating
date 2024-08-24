﻿using NoStringEvaluating.Factories;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Tests.UnitTests.Helpers;

internal class InternalValueFactory
{
    private readonly ValueKeeperContainer _valueKeeperContainer = new();

    public InternalValueFactory()
    {
        ValueFactory = new(_valueKeeperContainer);
    }

    public ValueFactory ValueFactory { get; }

    public List<InternalEvaluatorValue> Create(params EvaluatorValue[] externalValues)
    {
        return externalValues.Select(x => ValueFactory.Create(x)).ToList();
    }
}
