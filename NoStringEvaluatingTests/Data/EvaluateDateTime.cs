using System;
using System.Collections.Generic;
using System.Globalization;
using NoStringEvaluatingTests.Models;
using static NoStringEvaluatingTests.Helpers.FormulaModelFactory;

namespace NoStringEvaluatingTests.Data;

internal static class EvaluateDateTime
{
    public static IEnumerable<FormulaModel> Get()
    {
        var date1 = DateTime.Parse("02/12/2002", CultureInfo.InvariantCulture);
        var date2 = DateTime.Parse("07/18/2005", CultureInfo.InvariantCulture);

        yield return CreateTestModel("ToDateTime('02/12/2002')", date1);
        yield return CreateTestModel("ToDateTime('07/18/2005')", date2);
        yield return CreateTestModel("Today()", DateTime.Today);
    }
}
