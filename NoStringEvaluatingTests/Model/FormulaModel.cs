﻿using System.Collections.Generic;

namespace NoStringEvaluatingTests.Model
{
    public class FormulaModel
    {
        public string Formula { get; }
        public string ParsedFormula { get; }
        public double Result { get; }
        public bool ExpectedOkResult { get; }

        public Dictionary<string, double> Arguments { get; }

        public FormulaModel(string formula, string parsedFormula, double result, bool expectedOkResult = true)
        {
            Formula = formula;
            ParsedFormula = parsedFormula;
            Result = result;
            ExpectedOkResult = expectedOkResult;
            Arguments = new Dictionary<string, double>();
        }

        public override string ToString()
        {
            return Formula;
        }
    }
}
