using System.Collections.Generic;
using System.Linq;
using NoStringEvaluatingTests.Model;

namespace NoStringEvaluatingTests.Formulas
{
    public static partial class FormulasContainer
    {
        public static IEnumerable<FormulaModel[]> GetWordListFormulas()
        {
            var word = "one*two * three";
            var list1 = new[] { "one", "tWo" }.ToList();
            var list2 = new[] { "ONE", "tWo" }.ToList();
            var list3 = new[] { "a", "b", "c", "a", "c" }.ToList();

            // Explode
            yield return CreateTestModel("Explode(word; '*')", new[] { "one", "two ", " three" }.ToList(), ("word", word));
            yield return CreateTestModel("Explode(word)", new[] { "one*two", "*", "three" }.ToList(), ("word", word));

            // Lower
            yield return CreateTestModel("Lower(list)", new[] { "one", "two" }.ToList(), ("list", list1));

            // Replace
            yield return CreateTestModel("Replace(list; 'o'; '*')", new[] { "*ne", "tW*" }.ToList(), ("list", list1));

            // Qnique
            yield return CreateTestModel("Unique(list; true)", new[] { "b" }.ToList(), ("list", list3));
            yield return CreateTestModel("Unique(list; false)", new[] { "a", "b", "c" }.ToList(), ("list", list3));
            yield return CreateTestModel("Unique(list)", new[] { "a", "b", "c" }.ToList(), ("list", list3));

            // Upper
            yield return CreateTestModel("Upper(list)", new[] { "ONE", "TWO" }.ToList(), ("list", list2));

            // Sort
            yield return CreateTestModel("Sort(list)", list3.OrderBy(x => x).ToList(), ("list", list3));
            yield return CreateTestModel("Sort(list; 1)", list3.OrderBy(x => x).ToList(), ("list", list3));
            yield return CreateTestModel("Sort(list; -1)", list3.OrderByDescending(x => x).ToList(), ("list", list3));
        }

        public static IEnumerable<FormulaModel[]> GetWordListAsNumberFormulas()
        {
            var list1 = new[] { "one", "tWo" }.ToList();
            var list2 = new[] { "a", "b", "c", "a", "c" }.ToList();

            // Count
            yield return CreateTestModel("Count(list; '*')", 3, ("list", list1));
            yield return CreateTestModel("Count(list1; list2)", 7, ("list1", list1), ("list2", list2));
            yield return CreateTestModel("Count(1; 2)", 2);
            yield return CreateTestModel("Count(1; 'dd'; 2)", 3);
            yield return CreateTestModel("Count(1; 'dd'; 2; list)", 5, ("list", list1));
        }
    }
}
