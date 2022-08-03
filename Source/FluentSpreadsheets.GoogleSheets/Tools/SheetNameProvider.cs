using System.Text.RegularExpressions;

namespace FluentSpreadsheets.GoogleSheets.Tools;

public static class SheetNameProvider
{
    private static readonly Regex AlphabeticRegex = new("^[a-zA-Z0-9]*$", RegexOptions.Compiled);

    public static string GetSheetName(string title)
    {
        return AlphabeticRegex.IsMatch(title)
            ? title
            : $"'{title}'";
    }
}