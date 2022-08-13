﻿using System.Collections.Generic;
using NoStringEvaluating.Tests.Models;
using static NoStringEvaluating.Tests.Helpers.FormulaModelFactory;

namespace NoStringEvaluating.Tests.Data;

internal static class CheckFormula
{
    public static IEnumerable<FormulaModel> Get()
    {
        yield return CreateTestModelToCheck("+5", false);
        yield return CreateTestModelToCheck("5+", false);
        yield return CreateTestModelToCheck("-5", true);
        yield return CreateTestModelToCheck("-----5", true);
        yield return CreateTestModelToCheck("(-14)", true);
        yield return CreateTestModelToCheck("(+14)", false);
        yield return CreateTestModelToCheck("add(5; 6) - 6 * 55 / add(1; 2)", true);
        yield return CreateTestModelToCheck("5+6", true);
        yield return CreateTestModelToCheck("5;6", false);
        yield return CreateTestModelToCheck("5 + kov(5;6 - (5+11))", true);
        yield return CreateTestModelToCheck("5 + kov(5;6 - (5++11))", false);
        yield return CreateTestModelToCheck("5 + kov(5;6 - (5+11)))", false);
        yield return CreateTestModelToCheck("add(-add(2))", true);
        yield return CreateTestModelToCheck("add(-add(2/))", false);
        yield return CreateTestModelToCheck("add(-add(2)*--6)", true);
        yield return CreateTestModelToCheck("add(15 / -(-add(2)*--6))", true);
        yield return CreateTestModelToCheck("5+[dd] / mean(45;56;546)", true);
        yield return CreateTestModelToCheck("5+[dd] / mean(45;56; or(546 mean()))", false);
        yield return CreateTestModelToCheck("5+[dd] / mean(45;56 546)", false);
        yield return CreateTestModelToCheck("add(5; 6; ;)", false);
        yield return CreateTestModelToCheck("add(;)", false);
        yield return CreateTestModelToCheck("add()", true);
        yield return CreateTestModelToCheck("(56+6 / add(add(1; 5); 196^2))", true);
        yield return CreateTestModelToCheck("5^()", false);
        yield return CreateTestModelToCheck("or(1; 5) || 23 || 16 and(5; 6) && 1 and([a]; [b])", false);
        yield return CreateTestModelToCheck("1 and([a]; [b])", false);
        yield return CreateTestModelToCheck("(1) and([a]; [b])", false);
        yield return CreateTestModelToCheck("1 + and([a]; [b])", true);
        yield return CreateTestModelToCheck("and([a]; [b]) 1", false);
        yield return CreateTestModelToCheck("and([a]; [b]) / 1", true);
    }
}
