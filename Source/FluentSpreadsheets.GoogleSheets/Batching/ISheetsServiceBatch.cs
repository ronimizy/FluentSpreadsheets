using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Batching;

internal interface ISheetsServiceBatch
{
    void EnqueueValueUpdates(
        string spreadsheetId,
        IEnumerable<ValueRange> ranges,
        CancellationToken cancellationToken);

    void EnqueueSpreadsheetUpdates(
        string spreadsheetId,
        IEnumerable<Request> requests,
        CancellationToken cancellationToken);
}