namespace NoStringEvaluating.Tests.PerfTests.Report;

internal record ReportItem(string Formula, double Result, long ElapsedMilliseconds, long TargetElapsedMilliseconds)
{
    public bool Attention => ElapsedMilliseconds > TargetElapsedMilliseconds;
}
