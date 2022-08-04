using FluentSpreadsheets.GoogleSheets.Rendering;

namespace FluentSpreadsheets.GoogleSheets.Factories;

public interface IRenderCommandFactory
{
    Task<GoogleSheetRenderCommand> CreateAsync(string spreadsheetId, string title, IComponent component);

    Task<GoogleSheetRenderCommand> CreateAsync(string spreadsheetId, int id, IComponent component);
}