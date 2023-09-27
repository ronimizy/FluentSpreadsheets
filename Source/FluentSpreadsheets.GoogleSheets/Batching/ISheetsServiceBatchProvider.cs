namespace FluentSpreadsheets.GoogleSheets.Batching;

internal interface ISheetsServiceBatchProvider
{
    ISheetsServiceBatch? TryGetBatch();
}