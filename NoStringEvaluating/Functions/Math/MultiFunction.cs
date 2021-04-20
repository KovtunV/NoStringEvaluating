using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - multi
    /// </summary>
    public class MultiFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; } = "MULTI";

        /// <summary>
        /// Evaluate value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            var res = 1d;
            for (int i = 0; i < args.Count; i++)
            {
                var arg = args[i];
                if (arg.IsNumberList)
                {
                    var numberList = arg.GetNumberList();
                    for (int j = 0; j < numberList.Count; j++)
                    {
                        res *= numberList[j];
                    }

                    continue;
                }

                res *= args[i];
            }

            return res;
        }
    }
}
