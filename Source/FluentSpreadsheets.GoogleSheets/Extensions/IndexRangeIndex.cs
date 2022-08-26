using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Extensions;

internal static class IndexRangeExtensions
{
    public static GridRange ToGridRange(this IndexRange range, int sheetId)
    {
        return new GridRange
        {
            StartRowIndex = range.Start.Row - 1,
            StartColumnIndex = range.Start.Column - 1,
            EndRowIndex = range.End.Row - 1,
            EndColumnIndex = range.End.Column - 1,
            SheetId = sheetId,
        };
    }
}