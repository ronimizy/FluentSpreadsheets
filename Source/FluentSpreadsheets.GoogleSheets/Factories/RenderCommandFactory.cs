using FluentSpreadsheets.GoogleSheets.Rendering;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Factories;

public class RenderCommandFactory : IRenderCommandFactory
{
    private readonly SheetsService _sheetsService;

    public RenderCommandFactory(SheetsService sheetsService)
    {
        _sheetsService = sheetsService;
    }

    public async Task<GoogleSheetRenderCommand> CreateAsync(
        string spreadsheetId,
        string title,
        IComponent component,
        CancellationToken cancellationToken = default)
    {
        var id = await GetSheetId(spreadsheetId, title, cancellationToken);
        return new GoogleSheetRenderCommand(spreadsheetId, id, title, component);
    }

    public async Task<GoogleSheetRenderCommand> CreateAsync(
        string spreadsheetId,
        int id,
        IComponent component,
        CancellationToken cancellationToken = default)
    {
        var title = await GetSheetTitle(spreadsheetId, id, cancellationToken);
        return new GoogleSheetRenderCommand(spreadsheetId, id, title, component);
    }

    private async Task<int> GetSheetId(string spreadsheetId, string title, CancellationToken cancellationToken)
    {
        IList<Sheet> sheets = await GetSheetsAsync(spreadsheetId, cancellationToken);

        var sheet = sheets.FirstOrDefault(s => s.Properties.Title == title);

        if (sheet is null)
            throw new GoogleSheetsException($"Sheet with title {title} does not exist");

        var sheetId = sheet.Properties.SheetId;

        if (sheetId is null)
            throw new GoogleSheetsException("Sheet id does not exist");

        return sheetId.Value;
    }

    private async Task<string> GetSheetTitle(string spreadsheetId, int id, CancellationToken cancellationToken)
    {
        IList<Sheet> sheets = await GetSheetsAsync(spreadsheetId, cancellationToken);

        var sheet = sheets.FirstOrDefault(s => s.Properties.SheetId == id);

        if (sheet is null)
            throw new GoogleSheetsException($"Sheet with id {id} does not exist");

        return sheet.Properties.Title;
    }

    private async Task<IList<Sheet>> GetSheetsAsync(string spreadsheetId, CancellationToken cancellationToken)
    {
        var spreadSheet = await _sheetsService.Spreadsheets
            .Get(spreadsheetId)
            .ExecuteAsync(cancellationToken);

        return spreadSheet.Sheets;
    }
}