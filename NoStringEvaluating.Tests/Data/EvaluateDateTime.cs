using System.Globalization;
using NoStringEvaluating.Tests.Models;
using static NoStringEvaluating.Tests.Helpers.FormulaModelFactory;

namespace NoStringEvaluating.Tests.Data;

internal static class EvaluateDateTime
{
    public static IEnumerable<FormulaModel> Get()
    {
        var date1 = DateTime.Parse("02/12/2002", CultureInfo.InvariantCulture);
        var date2 = DateTime.Parse("07/18/2005", CultureInfo.InvariantCulture);

        yield return CreateTestModel("ToDateTime('02/12/2002')", date1);
        yield return CreateTestModel("ToDateTime('07/18/2005')", date2);
        yield return CreateTestModel("Today()", DateTime.Today);
        yield return CreateTestModel("AddHours(date; 17)", date1.AddHours(17), ("date", date1));
        yield return CreateTestModel("AddMinutes(date; 17)", date1.AddMinutes(17), ("date", date1));
        yield return CreateTestModel("AddSeconds(date; 17)", date1.AddSeconds(17), ("date", date1));
    }
}
