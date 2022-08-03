using FluentSpreadsheets.GoogleSheets.Models;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Tools;

public class GoogleSheetEditor
{
    private readonly SheetsService _sheetsService;
    private readonly string _spreadsheetId;

    public GoogleSheetEditor(SheetsService sheetsService, string spreadsheetId)
    {
        _sheetsService = sheetsService;
        _spreadsheetId = spreadsheetId;
    }

    public async Task UpdateValuesAsync(ValueRange valueRange, CancellationToken cancellationToken)
    {
        var updateRequest = new BatchUpdateValuesRequest
        {
            Data = new List<ValueRange> { valueRange },
            ValueInputOption = ValueInputOption.UserEntered
        };

        await _sheetsService.Spreadsheets.Values
            .BatchUpdate(updateRequest, _spreadsheetId)
            .ExecuteAsync(cancellationToken);
    }

    public Task ExecuteBatchUpdateAsync(Request request, CancellationToken cancellationToken)
        => ExecuteBatchUpdateAsync(new List<Request> { request }, cancellationToken);

    public async Task ExecuteBatchUpdateAsync(IList<Request> requests, CancellationToken cancellationToken)
    {
        Thread.Sleep(500);
        var batchUpdateRequest = new BatchUpdateSpreadsheetRequest
        {
            Requests = requests
        };

        await _sheetsService.Spreadsheets
            .BatchUpdate(batchUpdateRequest, _spreadsheetId)
            .ExecuteAsync(cancellationToken);
    }
}