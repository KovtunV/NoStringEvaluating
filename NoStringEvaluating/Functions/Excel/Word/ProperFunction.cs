using System.Collections.Generic;
using System.Globalization;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Excel.Word
{
    /// <summary>
    /// Capitalizes the first letter in each word of a text
    /// <para>Proper(myWord)</para>
    /// </summary>
    public class ProperFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "PROPER";

        /// <summary>
        /// Execute value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var word = args[0].GetWord();
            if (HasCapital(word))
            {
                word = word.ToLowerInvariant();
            }

            var res = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(word);
            return factory.Word().Create(res);
        }

        private bool HasCapital(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsUpper(str[i]))
                    return true;
            }

            return false;
        }
    }
}
