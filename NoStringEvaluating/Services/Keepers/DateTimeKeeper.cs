using System;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers.Base;

namespace NoStringEvaluating.Services.Keepers;

internal class DateTimeKeeper : BaseValueKeeper<DateTime>
{
    internal DateTimeKeeper() : base(ValueTypeKey.DateTime)
    {

    }

    internal static DateTimeKeeper Instance { get; } = new();
}
