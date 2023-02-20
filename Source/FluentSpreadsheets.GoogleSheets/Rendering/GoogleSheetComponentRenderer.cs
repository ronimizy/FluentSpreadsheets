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

    public Task RenderAsync(GoogleSheetRenderCommand command, CancellationToken cancellationToken = default)
    {
        var (spreadsheetId, id, title, component) = command;

        var handler = new GoogleSheetHandler(id, title);
        var visitor = new ComponentVisitor<GoogleSheetHandler>(new Index(1, 1), handler);
        component.Accept(visitor);

        return Task.WhenAll
        (
            UpdateStylesAsync(spreadsheetId, handler.StyleRequests, cancellationToken),
            UpdateValueRangesAsync(spreadsheetId, handler.ValueRanges, cancellationToken)
        );
    }

    private async Task UpdateValueRangesAsync(
        string spreadsheetId,
        IList<ValueRange> valueRanges,
        CancellationToken cancellationToken)
    {
        if (valueRanges.Count is 0)
            return;
        
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
        if (updateRequests.Count is 0)
            return;
        
        var batchUpdateRequest = new BatchUpdateSpreadsheetRequest
        {
            Requests = updateRequests,
        };

        await _sheetsService.Spreadsheets
            .BatchUpdate(batchUpdateRequest, spreadsheetId)
            .ExecuteAsync(cancellationToken);
    }
}