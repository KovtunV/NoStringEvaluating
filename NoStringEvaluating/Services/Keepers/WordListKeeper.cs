using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers.Base;

namespace NoStringEvaluating.Services.Keepers;

internal class WordListKeeper : BaseValueKeeper<List<string>>
{
    public WordListKeeper() : base(ValueTypeKey.WordList)
    {

    }

    public static WordListKeeper Instance { get; } = new();
}
