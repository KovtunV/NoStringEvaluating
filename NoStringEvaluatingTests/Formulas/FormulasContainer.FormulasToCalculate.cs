using System;
using System.Collections.Generic;
using NoStringEvaluatingTests.Model;

namespace NoStringEvaluatingTests.Formulas
{
    public static partial class FormulasContainer
    {
        public static IEnumerable<FormulaModel[]> GetFormulasToCalculate()
        {
            foreach (var val in GetCommonFormulas())
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
            yield return CreateTestModel("if(and(5; 8; 6) && [var1] < 5; 1; 0)", 0, ("var1", 5));
            yield return CreateTestModel("15+24 != [var1] * 3", 1, ("var1", 5));
            yield return CreateTestModel("15+24 == [var1] * 3", 1, ("var1", 13));
            yield return CreateTestModel("15+24 == [var1] * 3", 1, ("var1", 13));
            yield return CreateTestModel("15+24 == [var1] * 2", 0, ("var1", -3));
            yield return CreateTestModel("(5*3)-1", 14);
            yield return CreateTestModel("5*3-1", 14);
            yield return CreateTestModel("5*(3-1)", 10);
            yield return CreateTestModel("if(-1; -6; -7)", -6);
            yield return CreateTestModel("5 - -6", 11);
            yield return CreateTestModel("if ([Arg1] > 0; - [Arg1]; 0)", -16, ("Arg1", 16));
            yield return CreateTestModel("if ([Arg1] != 0; -----Arg1; 0)", -16, ("Arg1", 16));
            yield return CreateTestModel("-(5+6)", -11);
            yield return CreateTestModel("- add(1;3) - add(1; 2; 3)", -10);
            yield return CreateTestModel("if(5 > 0; -(5+6); 0)", -11);
            yield return CreateTestModel("-(9 - 7 + -(5 + 3))", 6);
            yield return CreateTestModel("-((5 + 6) * -(9 - 7 - (5 + 3))) * -((5 + 6) * -(9 - 7 - (5 + 3)))", 4356);
            yield return CreateTestModel("5 * -add(1; 3) * -[Arg1] / -(-add(1; 3) *3)", 1480, ("Arg1", 888));
            yield return CreateTestModel("5 * -add(1;3) * - 88 / -(-add(1; 16; 23; -(7+12)) *3)", 27.937);
            yield return CreateTestModel("-(5* -(5 / (6-7)+3))", -10);
            yield return CreateTestModel("-(5* -(5 * -(5+16) - (6-7 * -(5+16 * -(3+6)))+3))", 4325);
            yield return CreateTestModel("(5* -(5 * (5+16) - (6-7 * (5+16 * -(3+6)))+3))", 4355);
            yield return CreateTestModel("(5* -(5 * (5+16) - (6-7 * (5+16 * (3+6)))+3))", -5725);
            yield return CreateTestModel("(5* (5 * (5+16) - (6-7 * (5+16 * (3+6)))+3))", 5725);
            yield return CreateTestModel("1.56 *56.89 +8.3", 97.048);
            yield return CreateTestModel("1,56 *56,89 +8,3", 97.048);
            yield return CreateTestModel("add + [add] * add(1; 4)", 18, ("add", 3));
            yield return CreateTestModel("add * -add(1; 4)", -15, ("add", 3));
            yield return CreateTestModel("-add", -3, ("add", 3));
            yield return CreateTestModel("add", 3, ("add", 3));
            yield return CreateTestModel("add + add(add(5; add))", 11, ("add", 3));
            yield return CreateTestModel("15+24 == var1 * 2", 0, ("var1", -3));
            yield return CreateTestModel("15+24 == var_1 * 3", 1, ("var_1", 13));
            yield return CreateTestModel("- (my_name_is / 15)", -3, ("my_name_is", 45));
            yield return CreateTestModel("[myVariable ♥]", 30, ("myVariable ♥", 30));
            yield return CreateTestModel("Pi*pI/[PI] + -pI", 0);
            yield return CreateTestModel("if([tau] > 6 == true; 5+6; -9)", 11);
            yield return CreateTestModel("if(tAu > 6 == false; 5+6; -9)", -9);
            yield return CreateTestModel("e + [E]", Math.Round(Math.E + Math.E, 3));

            // Check functions
            foreach (var func in CheckFunctions()) yield return func;
        }

        private static IEnumerable<FormulaModel[]> CheckFunctions()
        {
            yield return CreateTestModel("abs(-89.7)", 89.7);
            yield return CreateTestModel("add(1; 2; 3; 7; -4)", 9);
            yield return CreateTestModel("ceil(1.6)", 2);
            yield return CreateTestModel("Fact(6)", 720);
            yield return CreateTestModel("Fib(16)", 987);
            yield return CreateTestModel("floor(1.6)", 1);
            yield return CreateTestModel("Gcd(56; 24; 6)", 2);
            yield return CreateTestModel("Lcm(12; 15; 10; 75)", 300);
            yield return CreateTestModel("Ln(12)", 2.485);
            yield return CreateTestModel("log10(100)", 2);
            yield return CreateTestModel("log2(2048)", 11);
            yield return CreateTestModel("log(2048; 2)", 11);
            yield return CreateTestModel("max(2048; 2; 897; 23000)", 23000);
            yield return CreateTestModel("mean(2048; 2; 897; 23000)", 6486.75);
            yield return CreateTestModel("min(2048; 2; 897; 23000 * -1)", -23000);
            yield return CreateTestModel("mod(5; 2)", 1);
            yield return CreateTestModel("multi(2048; 2; 897; 23000)", 84504576000);
            yield return CreateTestModel("sgn(5)", 1);
            yield return CreateTestModel("sgn(0)", 0);
            yield return CreateTestModel("sgn(-5)", -1);
            yield return CreateTestModel("sign(5)", 1);
            yield return CreateTestModel("sign(0)", 0);
            yield return CreateTestModel("sign(-5)", -1);
            yield return CreateTestModel("sqrt(169)", 13);
            yield return CreateTestModel("deg(-145.23)", -8321.066);
            yield return CreateTestModel("exp(-1)", 0.368);
            yield return CreateTestModel("rad(-145.23)", -2.535);
            yield return CreateTestModel("Arctan(-145.23)", -1.564);
            yield return CreateTestModel("Arctanh(0.23)", 0.234);
            yield return CreateTestModel("Arctg(0.23)", 0.226);
            yield return CreateTestModel("Arctgh(0.23)", 0.234);
            yield return CreateTestModel("Atan(0.23)", 0.226);
            yield return CreateTestModel("Atanh(0.23)", 0.234);
            yield return CreateTestModel("Atg(0.23)", 0.226);
            yield return CreateTestModel("Atgh(0.23)", 0.234);
            yield return CreateTestModel("tan(0.23)", 0.234);
            yield return CreateTestModel("tanh(0.23)", 0.226);
            yield return CreateTestModel("tg(0.23)", 0.234);
            yield return CreateTestModel("tgh(0.23)", 0.226);
            yield return CreateTestModel("arcsin(0.23)", 0.232);
            yield return CreateTestModel("arcsinh(0.23)", 0.228);
            yield return CreateTestModel("Arsin(0.23)", 0.232);
            yield return CreateTestModel("Arsinh(0.23)", 0.228);
            yield return CreateTestModel("Asin(0.23)", 0.232);
            yield return CreateTestModel("Asinh(0.23)", 0.228);
            yield return CreateTestModel("sin(1.23)", 0.942);
            yield return CreateTestModel("sinh(1.23)", 1.564);
            yield return CreateTestModel("Arcsec(12)", 1.487);
            yield return CreateTestModel("Arcsech(0.8)", 0.693);
            yield return CreateTestModel("Arsech(0.8)", 0.693);
            yield return CreateTestModel("Asech(0.8)", 0.693);
            yield return CreateTestModel("sec(0.8)", 1.435);
            yield return CreateTestModel("sech(0.8)", 0.748);
            yield return CreateTestModel("Acot(0.8)", 0.896);
            yield return CreateTestModel("Acoth(1.8)", 0.626);
            yield return CreateTestModel("Actan(0.8)", 0.896);
            yield return CreateTestModel("Actanh(1.8)", 0.626);
            yield return CreateTestModel("Actg(0.8)", 0.896);
            yield return CreateTestModel("Actgh(1.8)", 0.626);
            yield return CreateTestModel("Arccot(0.8)", 0.896);
            yield return CreateTestModel("Arccoth(1.8)", 0.626);
            yield return CreateTestModel("Arcctan(0.8)", 0.896);
            yield return CreateTestModel("Arcctanh(1.8)", 0.626);
            yield return CreateTestModel("Arcctg(0.8)", 0.896);
            yield return CreateTestModel("Arcctgh(1.8)", 0.626);
            yield return CreateTestModel("Cot(1.8)", -0.233);
            yield return CreateTestModel("Coth(1.8)", 1.056);
            yield return CreateTestModel("Ctan(1.8)", -0.233);
            yield return CreateTestModel("Ctanh(1.8)", 1.056);
            yield return CreateTestModel("Ctg(1.8)", -0.233);
            yield return CreateTestModel("Ctgh(1.8)", 1.056);
            yield return CreateTestModel("Acos(-0.8)", 2.498);
            yield return CreateTestModel("Acosh(18)", 3.583);
            yield return CreateTestModel("Arccos(-0.8)", 2.498);
            yield return CreateTestModel("Arccosh(18)", 3.583);
            yield return CreateTestModel("Arcos(-0.8)", 2.498);
            yield return CreateTestModel("Arcosh(18)", 3.583);
            yield return CreateTestModel("cos(18)", 0.66);
            yield return CreateTestModel("cosh(18)", 32829984.569);
            yield return CreateTestModel("Acosech(18)", 0.056);
            yield return CreateTestModel("Acsch(18)", 0.056);
            yield return CreateTestModel("Arccsch(18)", 0.056);
            yield return CreateTestModel("Arcosech(18)", 0.056);
            yield return CreateTestModel("Arcsch(18)", 0.056);
            yield return CreateTestModel("Cosec(18)", -1.332);
            yield return CreateTestModel("Cosech(3)", 0.1);
            yield return CreateTestModel("Csc(18)", -1.332);
            yield return CreateTestModel("Csch(3)", 0.1);
            yield return CreateTestModel("and(3 > 0; true == 1)", 1);
            yield return CreateTestModel("iff(3 < 0; 3; 3 > 0; 4)", 4);
            yield return CreateTestModel("if(3 < 0; 3; -1)", -1);
            yield return CreateTestModel("IsNaN(0 / 0)", 1);
            yield return CreateTestModel("not(false)", 1);
            yield return CreateTestModel("or(true; false)", 1);

            // IsNumber
            yield return CreateTestModel("IsNumber(5)", 1);
            yield return CreateTestModel("IsNumber('5')", 0);
            yield return CreateTestModel("IsNumber('my word')", 0);

            // ToNumber
            yield return CreateTestModel("ToNumber('5')", 5);
            yield return CreateTestModel("ToNumber('ghj5')", double.NaN);

            // IsError
            yield return CreateTestModel("IsError(ToNumber('Text'))", 1);
            yield return CreateTestModel("IsError(ToNumber('3'))", 0);

            // Word
            foreach (var item in GetWordAsNumberFormulas())
            {
                yield return item;
            }

            // DateTime
            foreach (var item in GetDateTimeAsNumberFormulas())
            {
                yield return item;
            }

            // WordList
            foreach (var item in GetWordListAsNumberFormulas())
            {
                yield return item;
            }

            // NumberList
            foreach (var item in GetNumberListAsNumberFormulas())
            {
                yield return item;
            }
        }
    }
}
