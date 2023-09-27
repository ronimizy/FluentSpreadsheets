using FluentSpreadsheets.GoogleSheets.Batching;
using FluentSpreadsheets.GoogleSheets.Handlers;
using FluentSpreadsheets.GoogleSheets.Models;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.GoogleSheets.Rendering;

internal class GoogleSheetComponentRenderer : IGoogleSheetsComponentRenderer
{
    private readonly ISheetsServiceExecutor _executor;

    public GoogleSheetComponentRenderer(ISheetsServiceExecutor executor)
    {
        _executor = executor;
    }

    public async Task RenderAsync(
        IComponent component,
        SheetInfo sheetInfo,
        Index startIndex,
        CancellationToken cancellationToken)
    {
        var (spreadsheetId, sheetId, sheetTitle) = sheetInfo;

        // Empty handler run is needed to compute labels
        var emptyHandler = new EmptyVisitorHandler();
        var emptyVisitor = new ComponentVisitor<EmptyVisitorHandler>(startIndex, emptyHandler);

        var handler = new GoogleSheetHandler(sheetId, sheetTitle);
        var visitor = new ComponentVisitor<GoogleSheetHandler>(startIndex, handler);

        component.Accept(emptyVisitor);
        component.Accept(visitor);

        await _executor.ExecuteValueUpdatesAsync(spreadsheetId, handler.ValueRanges, cancellationToken);
        await _executor.ExecuteSpreadsheetUpdatesAsync(spreadsheetId, handler.StyleRequests, cancellationToken);
    }

    public Task RenderAsync(
        IComponent component,
        SheetInfo sheetInfo,
        CancellationToken cancellationToken)
    {
        return RenderAsync(component, sheetInfo, new Index(1, 1), cancellationToken);
    }
}