namespace NoStringEvaluating.Tests.PerfTests.Report;

internal class ReportContainer
{
    public List<ReportItem> Items = [];

    public void Append(string formula, double result, long elapsedMilliseconds, long thresholdMilliseconds)
    {
        Items.Add(new ReportItem(formula, result, elapsedMilliseconds, thresholdMilliseconds));
    }
}
