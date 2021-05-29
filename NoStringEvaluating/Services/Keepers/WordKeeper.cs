using System.Collections.Generic;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Models.Values;
using NoStringEvaluating.Services.Keepers.Base;

namespace NoStringEvaluating.Services.Keepers
{
    internal class WordKeeper : BaseValueKeeper<string>
    {
        internal WordKeeper() : base(ValueTypeKey.Word)
        {

        }

        // Static 
        internal static WordKeeper Instance { get; }

        static WordKeeper()
        {
            Instance = new WordKeeper();
        }
    }
}
