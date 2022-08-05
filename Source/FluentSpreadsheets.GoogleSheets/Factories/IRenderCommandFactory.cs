using FluentSpreadsheets.GoogleSheets.Rendering;

namespace FluentSpreadsheets.GoogleSheets.Factories;

public interface IRenderCommandFactory
{
    Task<GoogleSheetRenderCommand> CreateAsync(
        string spreadsheetId,
        string title,
        IComponent component,
        CancellationToken cancellationToken = default);

    Task<GoogleSheetRenderCommand> CreateAsync(
        string spreadsheetId,
        int id,
        IComponent component,
        CancellationToken cancellationToken = default);
}