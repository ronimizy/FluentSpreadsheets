using FluentSpreadsheets.GoogleSheets.Models;

namespace FluentSpreadsheets.GoogleSheets.Factories;

public interface ISheetInfoFactory
{
    Task<SheetInfo> GetAsync(
        string spreadsheetId,
        string title,
        CancellationToken cancellationToken = default);

    Task<SheetInfo> GetAsync(
        string spreadsheetId,
        int id,
        IComponent component,
        CancellationToken cancellationToken = default);
}