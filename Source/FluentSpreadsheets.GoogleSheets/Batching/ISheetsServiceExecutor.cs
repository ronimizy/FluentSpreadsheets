using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Batching;

internal interface ISheetsServiceExecutor
{
    ValueTask ExecuteValueUpdatesAsync(
        string spreadsheetId,
        IEnumerable<ValueRange> ranges,
        CancellationToken cancellationToken);

    ValueTask ExecuteSpreadsheetUpdatesAsync(
        string spreadsheetId,
        IEnumerable<Request> requests,
        CancellationToken cancellationToken);
}