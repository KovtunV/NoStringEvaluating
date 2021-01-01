using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using NoStringEvaluating;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Extensions;
using NoStringEvaluating.Functions.Base;
using NoStringEvaluating.Services;
using org.mariuszgromada.math.mxparser;

namespace ConsoleApp
{
    public class Benchmark
    {
        private int _length;

        public Benchmark(int length)
        {
            _length = length;
        }

        #region NoString

        public void RunNoString(string formula)
        {
            // Init
            var container = new ServiceCollection().AddNoStringEvaluator();
            var services = container.BuildServiceProvider();
            var evaluator = services.GetRequiredService<INoStringEvaluator>();

            var functionReader = services.GetRequiredService<IFunctionReader>();
            functionReader.AddFunction(new Func_kov());
            functionReader.AddFunction(new Func_kovt());

            if (formula == Formula5)
            {
                RunNoString_Formula5(formula, evaluator);
                return;
            }

            if (formula == Formula6)
            {
                RunNoString_Formula6(formula, evaluator);
                return;
            }

            if (formula == Formula9)
            {
                RunNoString_Formula9(formula, evaluator);
                return;
            }

            // Idle running
            var idleRun = evaluator.Calc(formula);
            Console.WriteLine($"Formula result: {idleRun}");

            // Burning
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < _length; i++)
            {
                evaluator.Calc(formula);
            }

            PrintAppStates(sw);
        }

        private void RunNoString_Formula5(string formula, INoStringEvaluator evaluator)
        {
            // Init
            var args = new Dictionary<string, double>
            {
                [Arg1] = 84,
                [Arg2] = 56,
                [Arg3] = 1,
                [Arg4] = 2
            };

            // Idle running
            var idleRun = evaluator.Calc(formula, args);
            Console.WriteLine($"Formula result: {idleRun}");

            // Burning
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < _length; i++)
            {
                args[Arg1] = i;
                args[Arg2] = i + 1;
                args[Arg3] = i + 2;
                args[Arg4] = i + 3;

                evaluator.Calc(formula, args);
            }

            PrintAppStates(sw);
        }

        private void RunNoString_Formula6(string formula, INoStringEvaluator evaluator)
        {
            // Init
            var args = new Dictionary<string, double>
            {
                [Arg1] = 84,
                [Arg2] = 56,
                [Arg3] = 1,
                [Arg4] = 2,
                [Arg5] = 3,
                [Arg6] = 4,
                [Arg7] = 5,
                [Arg8] = 6,
                [Arg9] = 7,
                [Arg10] = 8,
            };

            // Idle running
            var idleRun = evaluator.Calc(formula, args);
            Console.WriteLine($"Formula result: {idleRun}");

            // Burning
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < _length; i++)
            {
                args[Arg1] = i + 1;
                args[Arg2] = i + 2;
                args[Arg3] = i + 3;
                args[Arg4] = i + 4;
                args[Arg5] = i + 5;
                args[Arg6] = i + 6;
                args[Arg7] = i + 7;
                args[Arg8] = i + 8;
                args[Arg9] = i + 9;
                args[Arg10] = i + 10;

                evaluator.Calc(formula, args);
            }

            PrintAppStates(sw);
        }

        private void RunNoString_Formula9(string formula, INoStringEvaluator evaluator)
        {
            // Init
            var args = new Dictionary<string, double> { [Arg1] = 1 };

            // Idle running
            var idleRun = evaluator.Calc(formula, args);
            Console.WriteLine($"Formula result: {idleRun}");

            // Burning
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < _length; i++)
            {
                args[Arg1] = i % 2 == 0 ? 1 : 0;

                evaluator.Calc(formula, args);
            }

            PrintAppStates(sw);
        }

        #endregion

        #region MxParser

        /// <summary>
        /// https://github.com/mariuszgromada/MathParser.org-mXparser
        /// </summary>
        public void RunMXParser(string formula)
        {
            var sourceFormula = formula;
            formula = KillNoStringBrackets(formula);

            // Init
            var expression = new Expression(formula);

            if (sourceFormula == Formula5)
            {
                RunMXParser_Formula5(expression);
                return;
            }

            if (sourceFormula == Formula6)
            {
                RunMXParser_Formula6(expression);
                return;
            }

            if (sourceFormula == Formula9)
            {
                RunMXParser_Formula9(expression);
                return;
            }

            if (sourceFormula == Formula10)
            {
                RunMXParser_Formula10(expression);
                return;
            }

            // Idle running
            var idleRun = expression.calculate();
            Console.WriteLine($"Formula result: {idleRun}");

            // Burning
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < _length; i++)
            {
                expression.calculate();
            }

            PrintAppStates(sw);
        }

        private void RunMXParser_Formula5(Expression expression)
        {
            // Init
            expression.defineArgument(Arg1, 84);
            expression.defineArgument(Arg2, 56);
            expression.defineArgument(Arg3, 1);
            expression.defineArgument(Arg4, 2);

            // Idle running
            var idleRun = expression.calculate();
            Console.WriteLine($"Formula result: {idleRun}");

            // Burning
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < _length; i++)
            {
                expression.setArgumentValue(Arg1, i);
                expression.setArgumentValue(Arg2, i + 1);
                expression.setArgumentValue(Arg3, i + 2);
                expression.setArgumentValue(Arg4, i + 3);

                expression.calculate();
            }

            PrintAppStates(sw);
        }

        private void RunMXParser_Formula6(Expression expression)
        {
            // Init
            expression.defineArgument(Arg1, 84);
            expression.defineArgument(Arg2, 56);
            expression.defineArgument(Arg3, 1);
            expression.defineArgument(Arg4, 2);
            expression.defineArgument(Arg5, 3);
            expression.defineArgument(Arg6, 4);
            expression.defineArgument(Arg7, 5);
            expression.defineArgument(Arg8, 6);
            expression.defineArgument(Arg9, 7);
            expression.defineArgument(Arg10, 8);

            // Idle running
            var idleRun = expression.calculate();
            Console.WriteLine($"Formula result: {idleRun}");

            // Burning
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < _length; i++)
            {
                expression.setArgumentValue(Arg1, i + 1);
                expression.setArgumentValue(Arg2, i + 2);
                expression.setArgumentValue(Arg3, i + 3);
                expression.setArgumentValue(Arg4, i + 4);
                expression.setArgumentValue(Arg5, i + 5);
                expression.setArgumentValue(Arg6, i + 6);
                expression.setArgumentValue(Arg7, i + 7);
                expression.setArgumentValue(Arg8, i + 8);
                expression.setArgumentValue(Arg9, i + 9);
                expression.setArgumentValue(Arg10, i + 10);

                expression.calculate();
            }

            PrintAppStates(sw);
        }

        private void RunMXParser_Formula9(Expression expression)
        {
            // Init
            expression.defineArgument(Arg1, 1);

            // Idle running
            var idleRun = expression.calculate();
            Console.WriteLine($"Formula result: {idleRun}");

            // Burning
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < _length; i++)
            {
                expression.setArgumentValue(Arg1, i % 2 == 0 ? 1 : 0);

                expression.calculate();
            }

            PrintAppStates(sw);
        }

        private void RunMXParser_Formula10(Expression expression)
        {
            // Init
            var f1 = new Function("kov", new FExtension_kov());
            var f2 = new Function("kovt", new FExtension_kovt());
            expression.addFunctions(f1, f2);

            // Idle running
            var idleRun = expression.calculate();
            Console.WriteLine($"Formula result: {idleRun}");

            // Burning
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < _length; i++)
            {
                expression.calculate();
            }

            PrintAppStates(sw);
        }

        #endregion

        private void PrintAppStates(Stopwatch sw)
        {
            sw.Stop();

            Console.WriteLine("");
            Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms.");
            Console.WriteLine($"Physical memory usage: {Environment.WorkingSet / 1024 / 1024} Mb");
            Console.WriteLine($"GC cycle count (Gen 0): {GC.CollectionCount(0)}");
            Console.WriteLine($"GC cycle count (Gen 1): {GC.CollectionCount(1)}");
            Console.WriteLine($"GC cycle count (Gen 2): {GC.CollectionCount(2)}\n");
        }

        private string KillNoStringBrackets(string formula)
        {
            return formula.Replace("[", string.Empty).Replace("]", string.Empty);
        }

        private const string Arg1 = "arg1";
        private const string Arg2 = "arg2";
        private const string Arg3 = "arg3";
        private const string Arg4 = "arg4";
        private const string Arg5 = "arg5";
        private const string Arg6 = "arg6";
        private const string Arg7 = "arg7";
        private const string Arg8 = "arg8";
        private const string Arg9 = "arg9";
        private const string Arg10 = "arg10";

        public static int One = 100_000;
        public static int Two = 1_000_000;
        public static int Three = 10_000_000;

        public static string Empty = "";
        public static string NumberOnly = "3";
        public static string Formula1 = "3 * 9";
        public static string Formula2 = "3 * 9 / 456 * 32 + 12 / 17 - 3";
        public static string Formula3 = "3 * (9 / 456 * (32 + 12)) / 17 - 3";
        public static string Formula4 = "(2 + 6 - (13 * 24 + 5 / (123 - 364 + 23))) - (2 + 6 - (13 * 24 + 5 / (123 - 364 + 23))) + (2 + 6 - (13 * 24 + 5 / (123 - 364 + 23))) * 345 * ((897 - 323)/ 23)";

        public static string Formula5 = $"[{Arg1}] * [{Arg2}] + [{Arg3}] - [{Arg4}]";
        public static string Formula6 = $"[{Arg1}] * ([{Arg2}] + [{Arg3}]) - [{Arg4}] / ([{Arg5}] - [{Arg6}]) + 45 * [{Arg7}] + (([{Arg8}] * 56 + (12 + [{Arg9}]))) - [{Arg10}]";

        public static string Formula7 = $"add(1; 2; 3)";
        public static string Formula8 = $"add(add(5; 1) - add(5; 2; 3))";
        public static string Formula9 = $"if([{Arg1}]; add(56 + 9 / 12 * 123.596; or(78; 9; 5; 2; 4; 5; 8; 7); 45;5); 9) *     24 + 52 -33";
        public static string Formula10 = $"kov(1; 2; 3) - kovt(8; 9)"; // 6 - -1 = 7
    }

    #region CustomFunctions

    public class Func_kov : IFunction
    {
        public string Name { get; } = "kov";

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
        public string Name { get; } = "kovt";

        public double Execute(IList<double> args)
        {
            return args[0] - args[1];
        }
    }

    public class FExtension_kov : FunctionExtensionVariadic
    {
        public double calculate(params double[] parameters)
        {
            var res = 1d;

            for (int i = 0; i < parameters.Length; i++)
            {
                res *= parameters[i];
            }

            return res;
        }

        public FunctionExtensionVariadic clone()
        {
            throw new NotImplementedException();
        }
    }

    public class FExtension_kovt : FunctionExtensionVariadic
    {
        public double calculate(params double[] parameters)
        {
            return parameters[0] - parameters[1];
        }

        public FunctionExtensionVariadic clone()
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
