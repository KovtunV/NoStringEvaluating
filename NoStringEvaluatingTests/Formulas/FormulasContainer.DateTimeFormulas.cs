using System;
using System.Collections.Generic;
using System.Globalization;
using NoStringEvaluatingTests.Model;

namespace NoStringEvaluatingTests.Formulas
{
    public static partial class FormulasContainer
    {
        public static IEnumerable<FormulaModel[]> GetDateTimeFormulas()
        {
            var date1 = DateTime.Parse("02/12/2002", CultureInfo.InvariantCulture);
            var date2 = DateTime.Parse("07/18/2005", CultureInfo.InvariantCulture);

            // ToDateTime
            yield return CreateTestModel("ToDateTime('02/12/2002')", date1);
            yield return CreateTestModel("ToDateTime('07/18/2005')", date2);

            // Today
            yield return CreateTestModel("Today()", DateTime.Today);
        }

        public static IEnumerable<FormulaModel[]> GetDateTimeAsNumberFormulas()
        {
            var date1 = DateTime.Parse("02/12/2002 14:18:23", CultureInfo.InvariantCulture);
            var date2 = DateTime.Parse("07/18/2005 18:30:10", CultureInfo.InvariantCulture);

            var time1 = DateTime.Parse("01/01/2000 14:18:23", CultureInfo.InvariantCulture);
            var time2 = DateTime.Parse("01/01/2000 18:30:10", CultureInfo.InvariantCulture);

            // DateDif
            yield return CreateTestModel("DateDif(date1; date2; 'Y')", 3, ("date1", date1), ("date2", date2));
            yield return CreateTestModel("DateDif(ToDateTime('12/02/2002'); ToDateTime('02/07/2005'); 'Y')", 2);
            yield return CreateTestModel("DateDif(date1; date2; 'M')", 41, ("date1", date1), ("date2", date2));
            yield return CreateTestModel("DateDif(date1; date2; 'D')", 1252, ("date1", date1), ("date2", date2));

            // TimeDif
            yield return CreateTestModel("Round(TimeDif(date1; date2; 'H'); 0)", 4, ("date1", time1), ("date2", time2));
            yield return CreateTestModel("Round(TimeDif(date1; date2; 'M'); 0)", 252, ("date1", time1), ("date2", time2));
            yield return CreateTestModel("Round(TimeDif(date1; date2; 'S'); 0)", 15107, ("date1", time1), ("date2", time2));

            // WeekDay
            yield return CreateTestModel("WeekDay(ToDateTime('04/18/2021'))", 1);
            yield return CreateTestModel("WeekDay(ToDateTime('04/19/2021'))", 2);
            yield return CreateTestModel("WeekDay(ToDateTime('04/20/2021'))", 3);
            yield return CreateTestModel("WeekDay(ToDateTime('04/21/2021'))", 4);
            yield return CreateTestModel("WeekDay(ToDateTime('04/22/2021'))", 5);
            yield return CreateTestModel("WeekDay(ToDateTime('04/23/2021'))", 6);
            yield return CreateTestModel("WeekDay(ToDateTime('04/24/2021'))", 7);
            yield return CreateTestModel("WeekDay(ToDateTime('04/25/2021'))", 1);
        }
    }
}
