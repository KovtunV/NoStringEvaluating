using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoStringEvaluating;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Functions;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluatingTests.Model;

namespace NoStringEvaluatingTests
{
    [TestClass]
    public class FormulaParserTests
    {
        private readonly IServiceProvider _serviceProvider;

        public FormulaParserTests()
        {
            _serviceProvider = new ServiceCollection().AddNoStringEvaluator().BuildServiceProvider();

            var functionReader = _serviceProvider.GetRequiredService<IFunctionReader>();
            functionReader.AddFunction(new Func_kov());
            functionReader.AddFunction(new Func_kovt());
        }

        [DynamicData(nameof(GetDataToParse), DynamicDataSourceType.Method)]
        [TestMethod]
        public void ParseFormula(FormulaModel model)
        {
            var parser = _serviceProvider.GetRequiredService<IFormulaParser>();
            var res = parser.Parse(model.Formula);

            Assert.AreEqual(model.ParsedFormula, res.ToString());
        }

        [DynamicData(nameof(GetDataToCalculate), DynamicDataSourceType.Method)]
        [TestMethod]
        public void CalculateFormula(FormulaModel model)
        {
            var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();

            var res = evaluator.Calc(model.Formula, model.Arguments);
            var roundedRes = Math.Round(res, 3);

            Assert.AreEqual(model.Result, roundedRes);
        }

        private static IEnumerable<FormulaModel[]> GetDataToCalculate()
        {
            foreach (var val in GetCommonData())
            {
                yield return val;
            }

            yield return CreateTestModel("2", 2);
            yield return CreateTestModel("add(5; 6; 9)", 20);
            yield return CreateTestModel("kov(1; 2; 3) - kovt(8; 9)", 7);
            yield return CreateTestModel("1/6 + 5/12 + 3/4 * 1/6 + 5/12 + 3/4 - 1/6 + 5/12 + 3/4- 78", -75.125);
            yield return CreateTestModel("(45^6 + (12 - (34*896^2) / 325) / 80000000) / 7^13 + 1.2", 1.286);
            yield return CreateTestModel("mean ([super power war]; 6; 6; 8; add(78;89;6;5;4;2;1;5;8;789;56;6;6)*7; 5; 2; 4; 87; 7; 89; 5; 4; 52; 3; 5; 4; 8; 78; 5; 4; 2; 3)", 
                357.739, ("super power war", 456));
            yield return CreateTestModel("[Provider(\"My test provider\").Month(-1).Price] * [Consumer(\"My test consumer\").Month().Volume]", 48,
                ("Provider(\"My test provider\").Month(-1).Price", 6), ("Consumer(\"My test consumer\").Month().Volume", 8));
            yield return CreateTestModel("if([var1] > 5 || [var1] != [var2]; 56+3; 1-344)", 59, ("var1", 5), ("var2", 6));
            yield return CreateTestModel("if([var1] >= 5 && [var1] + 10 == 15; 1; 0)", 1, ("var1", 5));
            yield return CreateTestModel("if(and(5, 8, 6) && [var1] < 5; 1; 0)", 0, ("var1", 5));
            yield return CreateTestModel("15+24 != [var1] * 3", 1, ("var1", 5));
            yield return CreateTestModel("15+24 == [var1] * 3", 1, ("var1", 13));
            yield return CreateTestModel("15+24 == [var1] * 3", 1, ("var1", 13));
            yield return CreateTestModel("15+24 == [var1] * 2", 0, ("var1", -3));
            yield return CreateTestModel("(5*3)-1", 14);
            yield return CreateTestModel("5*3-1", 14);
            yield return CreateTestModel("5*(3-1)", 10);
            yield return CreateTestModel("if(-1; -6; -7)", -6);
            yield return CreateTestModel("5 - -6", 11);
            yield return CreateTestModel("if ([Arg1] > 0; -[Arg1]; 0)", -16, ("Arg1", 16));
            yield return CreateTestModel("if ([Arg1] != 0; -----[Arg1]; 0)", -16, ("Arg1", 16));

            yield return CreateTestModel("-(5+6)", -11);
            yield return CreateTestModel("-add(1;3) - add(1; 2; 3)", -10);
            yield return CreateTestModel("if(5 > 0; -(5+6); 0)", -11);
            yield return CreateTestModel("-(9 - 7 + -(5 + 3))", 6);
            yield return CreateTestModel("-((5 + 6) * -(9 - 7 - (5 + 3))) * -((5 + 6) * -(9 - 7 - (5 + 3)))", 4356);
            yield return CreateTestModel("5 * -add(1; 3) * -[Arg1] / -(-add(1; 3) *3)", 1480, ("Arg1", 888));
            yield return CreateTestModel("5 * -add(1;3) * -88 / -(-add(1; 16; 23; -(7+12)) *3)", 27.937);
            yield return CreateTestModel("-(5* -(5 / (6-7)+3))", -10);
            yield return CreateTestModel("-(5* -(5 * -(5+16) - (6-7 * -(5+16 * -(3+6)))+3))", 4325);
            yield return CreateTestModel("(5* -(5 * (5+16) - (6-7 * (5+16 * -(3+6)))+3))", 4355);
            yield return CreateTestModel("(5* -(5 * (5+16) - (6-7 * (5+16 * (3+6)))+3))", -5725);
            yield return CreateTestModel("(5* (5 * (5+16) - (6-7 * (5+16 * (3+6)))+3))", 5725);
        }

        private static IEnumerable<FormulaModel[]> GetDataToParse()
        {
            return GetCommonData();
        }

        private static IEnumerable<FormulaModel[]> GetCommonData()
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

        private static FormulaModel[] CreateTestModel(string formula, double result, params (string, double)[] arguments)
        {
            return CreateTestModel(formula, "NULL", result, arguments);
        }

        private static FormulaModel[] CreateTestModel(string formula, string parsedFormula, double result, params (string, double)[] arguments)
        {
            var model = new FormulaModel(formula, parsedFormula, result);
            foreach (var argument in arguments)
            {
                model.Arguments[argument.Item1] = argument.Item2;
            }

            return new[] { model };
        }

        #region Utils

        private void Watch(Action act)
        {
            var sw = Stopwatch.StartNew();
            act();
            sw.Stop();
            Debug.WriteLine($"\nElapsed: {sw.ElapsedMilliseconds}ms.");
        }

        private void AppStates(Stopwatch sw = null)
        {
            Debug.WriteLine("");

            if (sw != null)
            {
                Debug.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms.");
            }

            Debug.WriteLine($"Physical memory usage: {Environment.WorkingSet / 1024 / 1024} Mb");
            Debug.WriteLine($"Allocated: {GC.GetTotalMemory(false) / 1024 / 1024} Mb");
            Debug.WriteLine($"GC cycle count (Gen 0): {GC.CollectionCount(0)}");
            Debug.WriteLine($"GC cycle count (Gen 1): {GC.CollectionCount(1)}");
            Debug.WriteLine($"GC cycle count (Gen 2): {GC.CollectionCount(2)}\n");
        }

        #endregion
    }

    #region Custom functions

    public class Func_kov : IFunction
    {
        public string Name { get; } = "KOV";

        public double Execute(IList<double> args)
        {
            var res = 1d;

            for (int i = 0; i < args.Count; i++)
            {
                res *= args[i];
            }

            return res;
        }
    }

    public class Func_kovt : IFunction
    {
        public string Name { get; } = "KOVT";

        public double Execute(IList<double> args)
        {
            return args[0] - args[1];
        }
    }

    #endregion
}