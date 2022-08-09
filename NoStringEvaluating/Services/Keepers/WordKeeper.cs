using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers.Base;

namespace NoStringEvaluating.Services.Keepers;

internal class WordKeeper : BaseValueKeeper<string>
{
    internal WordKeeper() : base(ValueTypeKey.Word)
    {

    }

    internal static WordKeeper Instance { get; } = new();
}
