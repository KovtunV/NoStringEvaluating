using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Exceptions;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Models;
using NoStringEvaluatingTests.Formulas;
using NoStringEvaluatingTests.Model;

namespace NoStringEvaluatingTests
{
    [TestClass]
    public class FormulaParserTests
    {
        private readonly IServiceProvider _serviceProvider;

        public FormulaParserTests()
        {
            _serviceProvider = new ServiceCollection().AddNoStringEvaluator(opt => opt.FloatingPointSymbol = FloatingPointSymbol.DotComma).BuildServiceProvider();

            var functionReader = _serviceProvider.GetRequiredService<IFunctionReader>();
            functionReader.AddFunction(new Func_kov());
            functionReader.AddFunction(new Func_kovt());
        }

        [DynamicData(nameof(GetFormulasToCheck), DynamicDataSourceType.Method)]
        [TestMethod]
        public void CheckFormula(FormulaModel model)
        {
            var parser = _serviceProvider.GetRequiredService<IFormulaChecker>();
            var res = parser.CheckSyntax(model.Formula);

            Assert.AreEqual(model.ExpectedOkResult, res.Ok);
        }

        [DynamicData(nameof(GetFormulasToParse), DynamicDataSourceType.Method)]
        [TestMethod]
        public void ParseFormula(FormulaModel model)
        {
            var parser = _serviceProvider.GetRequiredService<IFormulaParser>();
            var res = parser.Parse(model.Formula);

            Assert.AreEqual(model.ParsedFormula, res.ToString());
        }

        [DynamicData(nameof(GetFormulasToCalculate), DynamicDataSourceType.Method)]
        [TestMethod]
        public void CalculateFormula(FormulaModel model)
        {
            var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();

            var res = evaluator.Calc(model.Formula, model.Arguments);
            var roundedRes = Math.Round(res, 3);

            Assert.AreEqual(model.Result, roundedRes);
        }

        [TestMethod]
        public void VariableNotFoundException()
        {
            var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
            var vars = new Dictionary<string, double> { ["myVariable1"] = 7 };
            var formula = "myVariable + 5";

            try
            {
                var res = evaluator.Calc(formula, vars);
            }
            catch (VariableNotFoundException ex)
            {
                // This exception is OK
                Assert.AreEqual("myVariable", ex.VariableName);
            }

            // Now it should calculate     
            vars["myVariable"] = 7;
            var resOk = evaluator.Calc(formula, vars);
            Assert.AreEqual(12, resOk);
        }

        [TestMethod]
        public void VariableNotFoundExceptionNoDictionary()
        {
            var evaluator = _serviceProvider.GetRequiredService<INoStringEvaluator>();
            var formula = "[my var!] + 5";

            try
            {
                var res = evaluator.Calc(formula);
            }
            catch (VariableNotFoundException ex)
            {
                // This exception is OK
                Assert.AreEqual("my var!", ex.VariableName);
            }
        }

        #region DataSource

        private static IEnumerable<FormulaModel[]> GetFormulasToCheck()
            => FormulasContainer.GetFormulasToCheck();

        private static IEnumerable<FormulaModel[]> GetFormulasToParse()
            => FormulasContainer.GetFormulasToParse();

        private static IEnumerable<FormulaModel[]> GetFormulasToCalculate()
            => FormulasContainer.GetFormulasToCalculate();

        #endregion
    }

    #region Custom functions

    public class Func_kov : IFunction
    {
        public string Name { get; } = "KOV";

        public double Execute(List<double> args)
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

        public double Execute(List<double> args)
        {
            return args[0] - args[1];
        }
    }

    #endregion
}