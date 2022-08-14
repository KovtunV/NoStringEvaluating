using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers.Base;

namespace NoStringEvaluating.Services.Keepers;

internal class BooleanKeeper : BaseValueKeeper<bool>
{
    public BooleanKeeper() : base(ValueTypeKey.Boolean)
    {

    }

    public static BooleanKeeper Instance { get; } = new();
}
