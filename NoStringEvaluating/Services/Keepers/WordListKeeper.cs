using System.Collections.Generic;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers.Base;

namespace NoStringEvaluating.Services.Keepers;

internal class WordListKeeper : BaseValueKeeper<List<string>>
{
    internal WordListKeeper() : base(ValueTypeKey.WordList)
    {

    }

    internal static WordListKeeper Instance { get; } = new();
}
