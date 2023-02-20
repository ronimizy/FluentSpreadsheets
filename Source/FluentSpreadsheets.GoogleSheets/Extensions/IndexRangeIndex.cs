using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Extensions;

public static class IndexRangeExtensions
{
    public static IndexRange FromSpan(ReadOnlySpan<char> value)
    {
        var exclamationIndex = value.IndexOf('!');

        if (exclamationIndex is not -1)
        {
            value = value[exclamationIndex..];
        }

        var separatorIndex = value.IndexOf(':');

        if (separatorIndex is -1)
            throw new GoogleSheetsException("Invalid index range");

        ReadOnlySpan<char> from = value[..separatorIndex];
        ReadOnlySpan<char> to = value[(separatorIndex + 1)..];

        return new IndexRange(IndexExtensions.FromSpan(from), IndexExtensions.FromSpan(to));
    }

    internal static GridRange ToGridRange(this IndexRange range, int sheetId)
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