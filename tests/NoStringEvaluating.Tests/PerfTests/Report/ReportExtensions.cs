using System.Globalization;
using Spectre.Console;

namespace NoStringEvaluating.Tests.PerfTests.Report;

internal static class ReportExtensions
{
    public static Table AddRows(this Table table, IEnumerable<ReportItem> items)
    {
        foreach (var item in items)
        {
            table.AddRow(item);
        }

        return table;
    }

    public static void AddRow(this Table table, ReportItem value)
    {
        table.AddRow(
            value.Formula.ToText(),
            value.Result.ToText(),
            value.ElapsedMilliseconds.ToString().ToText(),
            value.TargetElapsedMilliseconds.ToString().ToText(),
            value.Attention ? "!".ToText() : string.Empty.ToText());
    }

    public static Text ToText(this double value)
    {
        return value.ToString("0.00", CultureInfo.InvariantCulture).ToText();
    }

    public static Text ToText(this string value)
    {
        return new Text(value);
    }
}
