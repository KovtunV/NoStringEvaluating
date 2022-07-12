using System.Collections.Generic;
using NoStringEvaluatingTests.Model;

namespace NoStringEvaluatingTests.Formulas;

public static partial class FormulasContainer
{
    public static IEnumerable<FormulaModel[]> GetCommonFormulas()
    {
        yield return CreateTestModel("5 + 6 * 13 / 2", "5 6 13 * 2 / +", 44);
        yield return CreateTestModel("256 / 32 / 4 * 2 + (256.346 / (32 / 4 * 2) + 256 / (32 / 4))", "256 32 / 4 / 2 * 256.346 32 4 / 2 * / 256 32 4 / / + +", 52.022);
        yield return CreateTestModel("(5)", "5", 5);
        yield return CreateTestModel("(5+6)", "5 6 +", 11);
        yield return CreateTestModel("256 / 32 / 4 * 2 + (256.346 / (32 / 4 * 2) + 256 / (32 / 4)) * 2^4", "256 32 / 4 / 2 * 256.346 32 4 / 2 * / 256 32 4 / / + 2 4 ^ * +", 772.346);
        yield return CreateTestModel("5 + aDd(78+6; 5; 6; 77+5) / 17", "5 ADD(78 6 +; 5; 6; 77 5 +) 17 / +", 15.412);
        yield return CreateTestModel("add(ADD(5*3; 6))", "ADD(ADD(5 3 *; 6))", 21);
        yield return CreateTestModel("78 + if(1; [my variable] * 9 /1; 1 - 3)", "78 IF(1; [my variable] 9 * 1 /; 1 3 -) +", 789, ("my variable", 79));
        yield return CreateTestModel("or(8 + 9 + 6)", "OR(8 9 + 6 +)", 1);
        yield return CreateTestModel("add(add(5) - 3)", "ADD(ADD(5) 3 -)", 2);
        yield return CreateTestModel("add(Add(5) - add(5))", "ADD(ADD(5) ADD(5) -)", 0);
        yield return CreateTestModel("add(add(5; 1) - add(5; 2; 3))", "ADD(ADD(5; 1) ADD(5; 2; 3) -)", -4);
        yield return CreateTestModel("add(add(5); add(5); and(8; 0))", "ADD(ADD(5); ADD(5); AND(8; 0))", 10);
        yield return CreateTestModel(
            "if([my variable]; add(56 + 9 / 12 * 123.596; or(78; 9; 5; 2; 4; 5; 8; 7); 45;5); 9) *     24 + 52 -33",
            "IF([my variable]; ADD(56 9 12 / 123.596 * +; OR(78; 9; 5; 2; 4; 5; 8; 7); 45; 5); 9) 24 * 52 + 33 -",
            4811.728, ("my variable", 1));

        yield return CreateTestModel("2 > 3", "2 3 >", 0);
        yield return CreateTestModel("3 >= 3", "3 3 >=", 1);
        yield return CreateTestModel("3 == 3", "3 3 ==", 1);
        yield return CreateTestModel("1 != 3", "1 3 !=", 1);
    }
}
