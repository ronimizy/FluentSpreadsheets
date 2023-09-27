namespace FluentSpreadsheets.GoogleSheets.Configuration;

public class GoogleSheetsBatchingConfiguration
{
    public GoogleSheetsBatchingConfiguration(int simultaneousRequestCount)
    {
        SimultaneousRequestCount = simultaneousRequestCount;
    }

    public int SimultaneousRequestCount { get; set; }
}