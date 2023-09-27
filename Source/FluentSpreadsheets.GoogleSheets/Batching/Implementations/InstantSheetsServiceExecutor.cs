using FluentSpreadsheets.GoogleSheets.Models;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Batching.Implementations;

public class InstantSheetsServiceExecutor : ISheetsServiceExecutor
{
    private readonly SheetsService _service;

    public InstantSheetsServiceExecutor(SheetsService service)
    {
        _service = service;
    }

    public async ValueTask ExecuteValueUpdatesAsync(
        string spreadsheetId,
        IEnumerable<ValueRange> ranges,
        CancellationToken cancellationToken)
    {
        ValueRange[] valueRanges = ranges.ToArray();

        if (valueRanges is [])
            return;

        var updateRequest = new BatchUpdateValuesRequest
        {
            Data = valueRanges,
            ValueInputOption = ValueInputOption.UserEntered,
        };

        await _service.Spreadsheets.Values
            .BatchUpdate(updateRequest, spreadsheetId)
            .ExecuteAsync(cancellationToken);
    }

    public async ValueTask ExecuteSpreadsheetUpdatesAsync(
        string spreadsheetId,
        IEnumerable<Request> requests,
        CancellationToken cancellationToken)
    {
        Request[] updateRequests = requests.ToArray();

        if (updateRequests is [])
            return;

        var batchUpdateRequest = new BatchUpdateSpreadsheetRequest
        {
            Requests = updateRequests,
        };

        await _service.Spreadsheets
            .BatchUpdate(batchUpdateRequest, spreadsheetId)
            .ExecuteAsync(cancellationToken);
    }
}