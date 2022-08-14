using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers.Base;

namespace NoStringEvaluating.Services.Keepers;

internal class NumberListKeeper : BaseValueKeeper<List<double>>
{
    public NumberListKeeper() : base(ValueTypeKey.NumberList)
    {

    }

    public static NumberListKeeper Instance { get; } = new();
}
