using FluentSpreadsheets.GoogleSheets.Models;
using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Factories;

internal static class DimensionRangeFactory
{
    public static DimensionRange Create(Dimension dimension, int startIndex, int endIndex, int sheetId)
    {
        return new DimensionRange
        {
            Dimension = dimension,
            StartIndex = startIndex - 1,
            EndIndex = endIndex - 1,
            SheetId = sheetId,
        };
    }
}