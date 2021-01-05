using System;
using System.Collections.Generic;
using System.Text;
using NoStringEvaluatingTests.Model;

namespace NoStringEvaluatingTests.Formulas
{
    public static partial class FormulasContainer
    {
        public static IEnumerable<FormulaModel[]> GetFormulasToParse()
        {
            return GetCommonFormulas();
        }
    }
}
