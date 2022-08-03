using Google.Apis.Sheets.v4;

namespace FluentSpreadsheets.GoogleSheets.Models;

public readonly record struct GoogleSheetArguments(
    SheetsService SheetService,
    string SpreadsheetId,
    int Id,
    string Title);