using FluentSpreadsheets.GoogleSheets.Models;

namespace FluentSpreadsheets.GoogleSheets.Rendering;

public interface IGoogleSheetsComponentRenderer
{
    Task RenderAsync(
        IComponent component,
        SheetInfo sheetInfo,
        Index startIndex,
        CancellationToken cancellationToken);
    
    Task RenderAsync(
        IComponent component,
        SheetInfo sheetInfo,
        CancellationToken cancellationToken);
}