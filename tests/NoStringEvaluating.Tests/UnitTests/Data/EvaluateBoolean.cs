using NoStringEvaluating.Tests.UnitTests.Models;
using static NoStringEvaluating.Tests.UnitTests.Helpers.FormulaModelFactory;

namespace NoStringEvaluating.Tests.UnitTests.Data;

internal static class EvaluateBoolean
{
    public static IEnumerable<FormulaModel> Get()
    {
        var myTrue = true;
        var myFalse = false;
        var list1 = new[] { "one", "tWo" }.ToList();

        yield return CreateTestModel("true == false", false);
        yield return CreateTestModel("true == true", true);
        yield return CreateTestModel("true = true", true);
        yield return CreateTestModel("true || false", true);
        yield return CreateTestModel("(true && false) || false", false);
        yield return CreateTestModel("(true && false) || false || 1 != 0", true);
        yield return CreateTestModel("(true || false) || false", true);
        yield return CreateTestModel("1 + 2 > 4", false);
        yield return CreateTestModel("1 + 2 > 2", true);
        yield return CreateTestModel("if(myTrue; false; true)", false, ("myTrue", myTrue));
        yield return CreateTestModel("Not([my false])", true, ("my false", myFalse));
        yield return CreateTestModel("15+24 != [var1] * 3", true, ("var1", 5));
        yield return CreateTestModel("15+24 == [var1] * 3", true, ("var1", 13));
        yield return CreateTestModel("15+24 == [var1] * 3", true, ("var1", 13));
        yield return CreateTestModel("15+24 == [var1] * 2", false, ("var1", -3));
        yield return CreateTestModel("2 > 3", false);
        yield return CreateTestModel("3 >= 3", true);
        yield return CreateTestModel("3 == 3", true);
        yield return CreateTestModel("1 != 3", true);
        yield return CreateTestModel("1 <> 3", true);
        yield return CreateTestModel("15+24 == var1 * 2", false, ("var1", -3));
        yield return CreateTestModel("15+24 == var_1 * 3", true, ("var_1", 13));
        yield return CreateTestModel("and(3 > 0; true == true)", true);
        yield return CreateTestModel("IsNaN(0 / 0)", true);
        yield return CreateTestModel("not(false)", true);
        yield return CreateTestModel("or(true; false)", true);
        yield return CreateTestModel("or(false)", false);
        yield return CreateTestModel("IsText(26)", false);
        yield return CreateTestModel("IsText('26')", true);
        yield return CreateTestModel("IsText('Hello!')", true);
        yield return CreateTestModel("IsNumber(26)", true);
        yield return CreateTestModel("IsNumber('26')", false);
        yield return CreateTestModel("IsNumber('Hello!')", false);
        yield return CreateTestModel("IsError(ToNumber('Text'))", true);
        yield return CreateTestModel("IsError(ToNumber('3'))", false);
        yield return CreateTestModel("NullIf(3;3) == null", true);
        yield return CreateTestModel("NullIf(4;3) == 4", true);
        yield return CreateTestModel("IfNull(thisisanullvar;'somethingelse') == 'somethingelse'", true);
        yield return CreateTestModel("IfNull('thisisnotnull';'somethingelse') == 'thisisnotnull'", true);
        yield return CreateTestModel("nuLl == 3", false);
        yield return CreateTestModel("nuLl == abc", true);
        yield return CreateTestModel("ToDateTime('04/18/2021') > ToDateTime('04/17/2021')", true);
        yield return CreateTestModel("ToDateTime('04/18/2021') < ToDateTime('04/17/2021')", false);
        yield return CreateTestModel("ToDateTime('04/17/2021')+1 > ToDateTime('04/17/2021')", true);
        yield return CreateTestModel("ToDateTime('04/17/2021')-1 < ToDateTime('04/17/2021')", true);
        yield return CreateTestModel("ToDateTime('04/17/2021') == ToDateTime('04/17/2021')", true);
        yield return CreateTestModel("IsMember({'printer', 'computer', 'monitor'};'computer')", true);
        yield return CreateTestModel("IsMember(list; 'one')", true, ("list", list1));
        yield return CreateTestModel("IsMember(list; 'onee')", false, ("list", list1));
        yield return CreateTestModel("IsNull(null)", true);
        yield return CreateTestModel("IsNull(5)", false);
        yield return CreateTestModel("!IsNull(null)", false);
        yield return CreateTestModel("!true", false);
        yield return CreateTestModel("!false", true);
        yield return CreateTestModel("!!true", true);
        yield return CreateTestModel("!!!true", false);
        yield return CreateTestModel("!myVar", false, ("myVar", true));
        yield return CreateTestModel("!myVar", true, ("myVar", false));
        yield return CreateTestModel("!!myVar", true, ("myVar", true));
        yield return CreateTestModel("!!!myVar", false, ("myVar", true));
        yield return CreateTestModel("!!![super var]", false, ("super var", true));
        yield return CreateTestModel("!And(true, true)", false);
        yield return CreateTestModel("!!And(true, true)", true);
        yield return CreateTestModel("!(5 > 2)", false);
        yield return CreateTestModel("!!(5 > 2)", true);
        yield return CreateTestModel("!!!(5 > 2)", false);
        yield return CreateTestModel("!(5 > 2) || !(5 + 6 = 13)", true);
    }
}
