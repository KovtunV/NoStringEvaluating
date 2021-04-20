using System.Collections.Generic;
using NoStringEvaluating.Factories;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Functions.Math
{
    /// <summary>
    /// Function - log
    /// </summary>
   public class LogFunction : IFunction
   {
       /// <summary>
       /// Name
       /// </summary>
       public virtual string Name { get; } = "LOG";

       /// <summary>
       /// Evaluate value
       /// </summary>
       public InternalEvaluatorValue Execute(List<InternalEvaluatorValue> args, ValueFactory factory)
       {
           return System.Math.Log(args[0], args[1]);
       }
    }
}
