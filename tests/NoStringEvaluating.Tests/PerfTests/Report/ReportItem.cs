namespace NoStringEvaluating.Tests.PerfTests.Report;

internal record ReportItem(string Formula, double Result, long ElapsedMilliseconds, long ThresholdMilliseconds)
{
    public bool Attention => ElapsedMilliseconds > ThresholdMilliseconds;
}
