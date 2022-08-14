using System;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers.Base;

namespace NoStringEvaluating.Services.Keepers;

internal class DateTimeKeeper : BaseValueKeeper<DateTime>
{
    public DateTimeKeeper() : base(ValueTypeKey.DateTime)
    {

    }

    public static DateTimeKeeper Instance { get; } = new();
}
