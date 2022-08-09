using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers.Base;

namespace NoStringEvaluating.Services.Keepers;

internal class NumberListKeeper : BaseValueKeeper<List<double>>
{
    internal NumberListKeeper() : base(ValueTypeKey.NumberList)
    {

    }

    internal static NumberListKeeper Instance { get; } = new();
}
