using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;
using static System.Math;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - gcd
    /// </summary>
    public sealed class GcdFunction : IFunction
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; } = "GCD";

        /// <summary>
        /// Can handle IsNull arguments?
        /// </summary>
        public bool CanHandleNullArguments { get; } = false;

        /// <summary>
        /// Evaluate value
        /// </summary>
        public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
        {
            if (args.Count == 1)
                return args[0];

            if (HasZero(args))
                return double.NaN;

            var res = GetGcd(args[0], args[1]);
            for (int i = 2; i < args.Count; i++)
            {
                res = GetGcd(res, args[i]);
            }

            return Abs(res);
        }

        private bool HasZero(List<InternalEvaluatorValue> args)
        {
            for (int i = 0; i < args.Count; i++)
            {
                if (Abs(args[i]) < NoStringEvaluatorConstants.FloatingTolerance)
                    return true;
            }

            return false;
        }

        private InternalEvaluatorValue GetGcd(InternalEvaluatorValue a, InternalEvaluatorValue b)
        {
            while (Abs(b) > NoStringEvaluatorConstants.FloatingTolerance)
            {
                var tmp = b;
                b = a % b;
                a = tmp;
            }

            return a;
        }
    }
}
