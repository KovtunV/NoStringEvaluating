using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers.Base;

namespace NoStringEvaluating.Services.Keepers;

internal class ObjectKeeper : BaseValueKeeper<object>
{
    public ObjectKeeper() : base(ValueTypeKey.Object)
    {

    }

    internal static ObjectKeeper Instance { get; } = new();
}
