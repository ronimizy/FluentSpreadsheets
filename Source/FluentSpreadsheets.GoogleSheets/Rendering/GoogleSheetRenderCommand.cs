using FluentSpreadsheets.Rendering;

namespace FluentSpreadsheets.GoogleSheets.Rendering;

public readonly record struct GoogleSheetRenderCommand(
        string SpreadsheetId, 
        int Id, 
        string Title, 
        IComponent Component)
    : IRenderCommand;