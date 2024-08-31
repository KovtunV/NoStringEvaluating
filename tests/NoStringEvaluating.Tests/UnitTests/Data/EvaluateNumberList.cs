using NoStringEvaluating.Tests.UnitTests.Models;
using static NoStringEvaluating.Tests.UnitTests.Helpers.FormulaModelFactory;

namespace NoStringEvaluating.Tests.UnitTests.Data;

internal static class EvaluateNumberList
{
    public static IEnumerable<FormulaModel> Get()
    {
        var list1 = new[] { 1d, 3d, 2d }.ToList();

        yield return CreateTestModel("Sort(list)", list1.OrderBy(x => x).ToList(), ("list", list1));
        yield return CreateTestModel("Sort(list; asc)", list1.OrderBy(x => x).ToList(), ("list", list1));
        yield return CreateTestModel("Sort(list; desc)", list1.OrderByDescending(x => x).ToList(), ("list", list1));
    }
}
