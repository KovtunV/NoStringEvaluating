using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers.Base;

namespace NoStringEvaluating.Services.Keepers;

internal class WordKeeper : BaseValueKeeper<string>
{
    public WordKeeper() : base(ValueTypeKey.Word)
    {

    }

    public static WordKeeper Instance { get; } = new();
}
