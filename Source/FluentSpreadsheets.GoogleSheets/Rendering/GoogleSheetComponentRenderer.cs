using FluentSpreadsheets.GoogleSheets.Handlers;
using FluentSpreadsheets.GoogleSheets.Models;
using FluentSpreadsheets.Rendering;
using FluentSpreadsheets.Visitors;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Rendering;

public class GoogleSheetComponentRenderer : IComponentRenderer<GoogleSheetRenderCommand>
{
    private readonly SheetsService _sheetsService;

    public GoogleSheetComponentRenderer(SheetsService sheetsService)
    {
        _sheetsService = sheetsService;
    }

    public async Task RenderAsync(GoogleSheetRenderCommand command, CancellationToken cancellationToken = default)
    {
        (string spreadsheetId, int id, string title, IComponent component) = command;

        var handler = new GoogleSheetHandler(id, title);
        var visitor = new ComponentVisitor<GoogleSheetHandler>(new Index(1, 1), handler);
        component.Accept(visitor);

        await UpdateValueRangesAsync(spreadsheetId, handler.ValueRanges, cancellationToken);
        await UpdateStylesAsync(spreadsheetId, handler.StyleRequests, cancellationToken);
    }

    private async Task UpdateValueRangesAsync(
        string spreadsheetId,
        IList<ValueRange> valueRanges,
        CancellationToken cancellationToken)
    {
        var updateRequest = new BatchUpdateValuesRequest
        {
            Data = valueRanges,
            ValueInputOption = ValueInputOption.UserEntered,
        };

        await _sheetsService.Spreadsheets.Values
            .BatchUpdate(updateRequest, spreadsheetId)
            .ExecuteAsync(cancellationToken);
    }

    private async Task UpdateStylesAsync(
        string spreadsheetId,
        IList<Request> updateRequests,
        CancellationToken cancellationToken)
    {
        var batchUpdateRequest = new BatchUpdateSpreadsheetRequest
        {
            Requests = updateRequests,
        };

        await _sheetsService.Spreadsheets
            .BatchUpdate(batchUpdateRequest, spreadsheetId)
            .ExecuteAsync(cancellationToken);
    }
}