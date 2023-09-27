namespace FluentSpreadsheets.GoogleSheets.Batching;

public interface ISheetsServiceBatchScopeFactory
{
    ISheetsServiceBatchScope CreateScope();
}