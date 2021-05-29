using NoStringEvaluatingTests.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace NoStringEvaluatingTests.Formulas
{
    public static partial class FormulasContainer
    {
        public static IEnumerable<FormulaModel[]> GetBooleanFormulas()
        {
            var myTrue = true;
            var myFalse = false;

            yield return CreateTestModel("true || false", true);
            yield return CreateTestModel("(true && false) || false", false);
            yield return CreateTestModel("(true && false) || false || 1 != 0", true);
            yield return CreateTestModel("(true || false) || false", true);
            yield return CreateTestModel("1 + 2 > 4", false);
            yield return CreateTestModel("1 + 2 > 2", true);
            yield return CreateTestModel("if(myTrue; 0; 1)", false, ("myTrue", myTrue));
            yield return CreateTestModel("Not([my false])", true, ("my false", myFalse));

            // IsText
            yield return CreateTestModel("IsText(26)", false);
            yield return CreateTestModel("IsText('26')", true);
            yield return CreateTestModel("IsText('Hello!')", true);

            // IsNumber
            yield return CreateTestModel("IsNumber(26)", true);
            yield return CreateTestModel("IsNumber('26')", false);
            yield return CreateTestModel("IsNumber('Hello!')", false);
        }
    }
}
