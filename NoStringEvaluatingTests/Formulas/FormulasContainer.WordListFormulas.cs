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
            yield return CreateTestModel("Lower({'Mumbai','London','New York'})", new[] { "mumbai", "london", "new york" }.ToList());

            // Replace
            yield return CreateTestModel("Replace(list; 'o'; '*')", new[] { "*ne", "tW*" }.ToList(), ("list", list1));
            yield return CreateTestModel("Replace({'New','York','City'}; 'New'; 'Old')", new[] { "Old", "York", "City"}.ToList());

            // Qnique
            yield return CreateTestModel("Unique(list; true)", new[] { "b" }.ToList(), ("list", list3));
            yield return CreateTestModel("Unique(list; false)", new[] { "a", "b", "c" }.ToList(), ("list", list3));
            yield return CreateTestModel("Unique(list)", new[] { "a", "b", "c" }.ToList(), ("list", list3));
            yield return CreateTestModel("Unique({'NEW','OLD','NEW','HEAVEN','OLD'})", new[] { "NEW", "OLD", "HEAVEN" }.ToList());
            yield return CreateTestModel("Unique({'NEW','OLD','NEW','HEAVEN','OLD'}; true)", new[] { "HEAVEN" }.ToList());

            // Upper
            yield return CreateTestModel("Upper(list)", new[] { "ONE", "TWO" }.ToList(), ("list", list2));
            yield return CreateTestModel("Upper({'Mumbai','London','New York'})", new[] { "MUMBAI", "LONDON", "NEW YORK" }.ToList());

            // Sort
            yield return CreateTestModel("Sort(list)", list3.OrderBy(x => x).ToList(), ("list", list3));
            yield return CreateTestModel("Sort(list; 1)", list3.OrderBy(x => x).ToList(), ("list", list3));
            yield return CreateTestModel("Sort(list; -1)", list3.OrderByDescending(x => x).ToList(), ("list", list3));
            yield return CreateTestModel("Sort({ \"a\", \"b\", \"c\", \"a\", \"c\" })", list3.OrderBy(x => x).ToList());
            yield return CreateTestModel("Sort({ \"a\", \"b\", \"c\", \"a\", \"c\" }; ASC)", list3.OrderBy(x => x).ToList());
            yield return CreateTestModel("Sort({ \"a\", \"b\", \"c\", \"a\", \"c\" }; DESC)", list3.OrderByDescending(x => x).ToList());

            // Left
            yield return CreateTestModel("Left({'Analogy', 'Anecdote', 'Anomaly'}; 3)", new []{ "Ana", "Ane", "Ano" }.ToList());
            yield return CreateTestModel("Left({'Analogy', 'Anecdote', 'Anomaly'}; 'n')", new []{ "A", "A", "A" }.ToList());

            // Middle
            yield return CreateTestModel("Middle({'Analogy', 'Anecdote', 'Anomaly'}; 3; 2)", new[] { "lo", "cd", "ma" }.ToList());
            yield return CreateTestModel("Middle({'Analogy', 'Anecdote', 'Anomaly'}; 'n'; 'o')", new[] { "al", "ecd", "" }.ToList());

            // Right
            yield return CreateTestModel("Right({'Analogy', 'Anecdote', 'Anomaly'}; 3)", new[] { "ogy", "ote", "aly" }.ToList());
            yield return CreateTestModel("Right({'Analogy', 'Anecdote', 'Anomaly'}; 'n')", new[] { "alogy", "ecdote", "omaly" }.ToList());
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
            yield return CreateTestModel("Count(1; 2; {1, 5, 6, 3, 7})", 7);
            yield return CreateTestModel("Count(1; 2; {'one' 'two' 'thrte'})", 5);

            // IsMember
            yield return CreateTestModel("IsMember({'printer', 'computer', 'monitor'};'computer')", 1);
            yield return CreateTestModel("IsMember(list; 'one')", 1, ("list", list1));
            yield return CreateTestModel("IsMember(list; 'onee')", 0, ("list", list1));

        }
    }
}
