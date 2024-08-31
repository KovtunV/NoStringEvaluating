using NUnit.Framework;
using Spectre.Console;

namespace NoStringEvaluating.Tests.PerfTests.Report;

internal class ReportWriter
{
    private const string DIRECTORY = "PerformanceTestsResults";
    private const string FILE_PATH = $"{DIRECTORY}/PerformanceTests.txt";

    public static void Write(ReportContainer resultsContainer)
    {
        if (!Directory.Exists(DIRECTORY))
        {
            Directory.CreateDirectory(DIRECTORY);
        }

        using var streamWriter = new StreamWriter(FILE_PATH);

        var table = new Table()
            .SquareBorder()
            .AddColumns("Formula", "Result", "Elapsed time, ms", "Threshold, ms")
            .AddColumn(new TableColumn($"Attention").Centered())
            .AddRows(resultsContainer.Items);

        AnsiConsole.Console.Profile.Width = 100;

        AnsiConsole.Record();
        AnsiConsole.Write(table);

        var text = AnsiConsole.ExportText();

        TestContext.Progress.WriteLine(text);
        streamWriter.WriteLine(text);
    }
}
