using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - mean
    /// </summary>
    public class MeanFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "MEAN";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var count = 0;
            var sum = 0d;
            for (int i = 0; i < args.Count; i++)
            {
                var arg = args[i];
                if (arg.IsNumberList)
                {
                    var numberList = arg.GetNumberList();
                    for (int j = 0; j < numberList.Count; j++)
                    {
                        sum += numberList[j];
                    }

                    count += numberList.Count;
                    continue;
                }

                sum += args[i];
                count++;
            }

            return sum / count;
        }
    }
}
