using System.Text;

namespace FluentSpreadsheets.GoogleSheets.Extensions;

internal static class IndexExtensions
{
    public static string ToGoogleSheetsIndex(this Index index, string sheetName)
    {
        return $"{sheetName}!{GetColumnIndex(index.Column)}{index.Row}";
    }

    private static string GetColumnIndex(int value)
    {
        if (value is 0)
            return string.Empty;

        var builder = new StringBuilder();

        while (value > 0)
        {
            int mod = (value - 1) % 26;
            builder.Append((char)('A' + mod));
            value = (value - mod) / 26;
        }

        return builder.ToString();
    }
}