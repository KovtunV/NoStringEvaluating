using System.Collections.Generic;
using System.Linq;
using NoStringEvaluatingTests.Models;
using static NoStringEvaluatingTests.Helpers.FormulaModelFactory;

namespace NoStringEvaluatingTests.Data;

internal static class EvaluateWordList
{
    public static IEnumerable<FormulaModel> Get()
    {
        var word = "one*two * three";
        var list1 = new[] { "one", "tWo" }.ToList();
        var list2 = new[] { "ONE", "tWo" }.ToList();
        var list3 = new[] { "a", "b", "c", "a", "c" }.ToList();

        yield return CreateTestModel("Explode(word; '*')", new[] { "one", "two ", " three" }.ToList(), ("word", word));
        yield return CreateTestModel("Explode(word)", new[] { "one*two", "*", "three" }.ToList(), ("word", word));
        yield return CreateTestModel("Lower(list)", new[] { "one", "two" }.ToList(), ("list", list1));
        yield return CreateTestModel("Lower({'Mumbai','London','New York'})", new[] { "mumbai", "london", "new york" }.ToList());
        yield return CreateTestModel("Replace(list; 'o'; '*')", new[] { "*ne", "tW*" }.ToList(), ("list", list1));
        yield return CreateTestModel("Replace({'New','York','City'}; 'New'; 'Old')", new[] { "Old", "York", "City" }.ToList());
        yield return CreateTestModel("Unique(list; true)", new[] { "b" }.ToList(), ("list", list3));
        yield return CreateTestModel("Unique(list; false)", new[] { "a", "b", "c" }.ToList(), ("list", list3));
        yield return CreateTestModel("Unique(list)", new[] { "a", "b", "c" }.ToList(), ("list", list3));
        yield return CreateTestModel("Unique({'NEW','OLD','NEW','HEAVEN','OLD'})", new[] { "NEW", "OLD", "HEAVEN" }.ToList());
        yield return CreateTestModel("Unique({'NEW','OLD','NEW','HEAVEN','OLD'}; true)", new[] { "HEAVEN" }.ToList());
        yield return CreateTestModel("Upper(list)", new[] { "ONE", "TWO" }.ToList(), ("list", list2));
        yield return CreateTestModel("Upper({'Mumbai','London','New York'})", new[] { "MUMBAI", "LONDON", "NEW YORK" }.ToList());
        yield return CreateTestModel("Sort(list)", list3.OrderBy(x => x).ToList(), ("list", list3));
        yield return CreateTestModel("Sort(list; asc)", list3.OrderBy(x => x).ToList(), ("list", list3));
        yield return CreateTestModel("Sort(list; desc)", list3.OrderByDescending(x => x).ToList(), ("list", list3));
        yield return CreateTestModel("Sort({ \"a\", \"b\", \"c\", \"a\", \"c\" })", list3.OrderBy(x => x).ToList());
        yield return CreateTestModel("Sort({ \"a\", \"b\", \"c\", \"a\", \"c\" }; ASC)", list3.OrderBy(x => x).ToList());
        yield return CreateTestModel("Sort({ \"a\", \"b\", \"c\", \"a\", \"c\" }; DESC)", list3.OrderByDescending(x => x).ToList());
        yield return CreateTestModel("Left({'Analogy', 'Anecdote', 'Anomaly'}; 3)", new[] { "Ana", "Ane", "Ano" }.ToList());
        yield return CreateTestModel("Left({'Analogy', 'Anecdote', 'Anomaly'}; 'n')", new[] { "A", "A", "A" }.ToList());
        yield return CreateTestModel("Middle({'Analogy', 'Anecdote', 'Anomaly'}; 3; 2)", new[] { "lo", "cd", "ma" }.ToList());
        yield return CreateTestModel("Middle({'Analogy', 'Anecdote', 'Anomaly'}; 'n'; 'o')", new[] { "al", "ecd", string.Empty }.ToList());
        yield return CreateTestModel("Right({'Analogy', 'Anecdote', 'Anomaly'}; 3)", new[] { "ogy", "ote", "aly" }.ToList());
        yield return CreateTestModel("Right({'Analogy', 'Anecdote', 'Anomaly'}; 'n')", new[] { "alogy", "ecdote", "omaly" }.ToList());
    }
}
