namespace FluentSpreadsheets.GoogleSheets.Extensions;

public static class IndexExtensions
{
    public static string ToGoogleSheetsIndex(this Index index, string sheetTitle) => $"{sheetTitle}!{index}";

    public static Index FromSpan(ReadOnlySpan<char> value) => Index.FromSpan(value);
}
