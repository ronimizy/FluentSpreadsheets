using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Batching.Implementations;

internal class BatchingSheetsServiceExecutor : ISheetsServiceExecutor
{
    private readonly ISheetsServiceBatchProvider _batchProvider;
    private readonly InstantSheetsServiceExecutor _instantExecutor;

    public BatchingSheetsServiceExecutor(ISheetsServiceBatchProvider batchProvider, SheetsService service)
    {
        _batchProvider = batchProvider;
        _instantExecutor = new InstantSheetsServiceExecutor(service);
    }

    public ValueTask ExecuteValueUpdatesAsync(
        string spreadsheetId,
        IEnumerable<ValueRange> ranges,
        CancellationToken cancellationToken)
    {
        var batch = _batchProvider.TryGetBatch();

        if (batch is null)
            return _instantExecutor.ExecuteValueUpdatesAsync(spreadsheetId, ranges, cancellationToken);

        batch.EnqueueValueUpdates(spreadsheetId, ranges, cancellationToken);

        return default;
    }

    public ValueTask ExecuteSpreadsheetUpdatesAsync(
        string spreadsheetId,
        IEnumerable<Request> requests,
        CancellationToken cancellationToken)
    {
        var batch = _batchProvider.TryGetBatch();

        if (batch is null)
            return _instantExecutor.ExecuteSpreadsheetUpdatesAsync(spreadsheetId, requests, cancellationToken);

        batch.EnqueueSpreadsheetUpdates(spreadsheetId, requests, cancellationToken);

        return default;
    }
}