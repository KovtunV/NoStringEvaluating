using System.Collections.Generic;
using System.Linq;
using NoStringEvaluatingTests.Model;

namespace NoStringEvaluatingTests.Formulas;

public static partial class FormulasContainer
{
    public static IEnumerable<FormulaModel[]> GetNumberListFormulas()
    {
        var list1 = new[] { 1d, 3d, 2d }.ToList();

        // Sort
        yield return CreateTestModel("Sort(list)", list1.OrderBy(x => x).ToList(), ("list", list1));
        yield return CreateTestModel("Sort(list; 1)", list1.OrderBy(x => x).ToList(), ("list", list1));
        yield return CreateTestModel("Sort(list; -1)", list1.OrderByDescending(x => x).ToList(), ("list", list1));
    }

    public static IEnumerable<FormulaModel[]> GetNumberListAsNumberFormulas()
    {
        var list1 = new[] { 1d, 3d, 2d }.ToList();
        var list2 = new[] { 1d, 3d, 2d, 14d }.ToList();

        // Count
        yield return CreateTestModel("Count(list)", 3, ("list", list1));
        yield return CreateTestModel("Count(list; 1; 1)", 5, ("list", list1));
        yield return CreateTestModel("Count(8; 'j'; list; 1; 1)", 7, ("list", list1));

        // Add
        yield return CreateTestModel("Add(list)", 6, ("list", list1));
        yield return CreateTestModel("Add(10; list)", 16, ("list", list1));
        yield return CreateTestModel("Add(10; list; 90)", 106, ("list", list1));
        yield return CreateTestModel("Add(list; list)", 12, ("list", list1));

        // Max
        yield return CreateTestModel("Max(list)", 3, ("list", list1));
        yield return CreateTestModel("Max(list; 300)", 300, ("list", list1));
        yield return CreateTestModel("Max(2; 3; 4; 1; list)", 4, ("list", list1));
        yield return CreateTestModel("Max(2; 3; -4; 1; list; 2)", 3, ("list", list1));
        yield return CreateTestModel("Max(2; 3; -4; 1; list; 12)", 12, ("list", list1));
        yield return CreateTestModel("Max(2; 3; -4; 1; list1; 12; list2; 10)", 14, ("list1", list1), ("list2", list2));

        // Mean
        yield return CreateTestModel("Mean(list)", 2, ("list", list1));
        yield return CreateTestModel("Mean(2; 3; 4; 1; list)", 2.286, ("list", list1));
        yield return CreateTestModel("Mean(2; 3; -4; 1; list; 2)", 1.25, ("list", list1));
        yield return CreateTestModel("Mean(2; 3; -4; 1; list; 12)", 2.5, ("list", list1));
        yield return CreateTestModel("Mean(2; 3; -4; 1; list1; 12; list2; 10)", 3.846, ("list1", list1), ("list2", list2));

        // Min
        yield return CreateTestModel("Min(list)", 1, ("list", list1));
        yield return CreateTestModel("Min(list; -17)", -17, ("list", list1));
        yield return CreateTestModel("Min(2; 3; 4; list)", 1, ("list", list1));
        yield return CreateTestModel("Min(2; 3; -4; list; 2)", -4, ("list", list1));
        yield return CreateTestModel("Min(2; 3; -4; 1; list; 12)", -4, ("list", list1));
        yield return CreateTestModel("Min(2; 3; -4; 1; list1; 12; list2; -10)", -10, ("list1", list1), ("list2", list2));

        // Multi
        yield return CreateTestModel("Multi(list; 3)", 18, ("list", list1));
        yield return CreateTestModel("Multi(10; list)", 60, ("list", list1));
        yield return CreateTestModel("Multi(10; list; 90)", 5400, ("list", list1));
        yield return CreateTestModel("Multi(list; list)", 36, ("list", list1));
    }
}
