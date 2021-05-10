using System.Collections.Generic;
using static NoStringEvaluating.NoStringEvaluatorConstants;

namespace NoStringEvaluating.Services.Value
{
    internal static class WordFormatter
    {
        public static string Format(string word)
        {
            if (UseWordQuotationMark)
            {
                return $"{WordQuotationMark}{word}{WordQuotationMark}";
            }

            return word;
        }

        public static List<string> Format(List<string> list)
        {
            if(list is null)
            {
                return list;
            }

            if (UseWordQuotationMark)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = $"{WordQuotationMark}{list[i]}{WordQuotationMark}";
                }
            }

            return list;
        }
    }
}
