using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - multi
    /// </summary>
    public sealed class MultiFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; } = "MULTI";

        /// <summary>
        /// Can handle IsNull arguments?
        /// </summary>
        public bool CanHandleNullArguments { get; } = false;

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
