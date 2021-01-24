using System.Collections.Generic;
using NoStringEvaluating.Functions.Base;

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
       public double Execute(List<double> args)
       {
           return System.Math.Log(args[0], args[1]);
       }
    }
}
