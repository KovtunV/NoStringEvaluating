using System.Collections.Generic;
using System.Linq;
using NoStringEvaluatingTests.Models;
using static NoStringEvaluatingTests.Helpers.FormulaModelFactory;

namespace NoStringEvaluatingTests.Data;

internal static class EvaluateNumberList
{
    public static IEnumerable<FormulaModel> Get()
    {
        var list1 = new[] { 1d, 3d, 2d }.ToList();

        yield return CreateTestModel("Sort(list)", list1.OrderBy(x => x).ToList(), ("list", list1));
        yield return CreateTestModel("Sort(list; 1)", list1.OrderBy(x => x).ToList(), ("list", list1));
        yield return CreateTestModel("Sort(list; -1)", list1.OrderByDescending(x => x).ToList(), ("list", list1));
    }
}
