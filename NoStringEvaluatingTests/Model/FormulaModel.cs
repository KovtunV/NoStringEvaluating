using System;
using System.Collections.Generic;
using System.Text;

namespace NoStringEvaluatingTests.Model
{
    public class FormulaModel
    {
        public string Formula { get; }
        public string ParsedFormula { get; }
        public double Result { get; }

        public Dictionary<string, double> Arguments { get; }

        public FormulaModel(string formula, string parsedFormula, double result)
        {
            Formula = formula;
            ParsedFormula = parsedFormula;
            Result = result;
            Arguments = new Dictionary<string, double>();
        }

        public override string ToString()
        {
            return Formula;
        }
    }
}
