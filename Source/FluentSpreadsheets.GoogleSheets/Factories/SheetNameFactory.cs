using System.Text.RegularExpressions;

namespace FluentSpreadsheets.GoogleSheets.Factories;

internal class SheetNameFactory
{
    private static readonly Regex AlphabeticRegex = new("^[a-zA-Z0-9]*$", RegexOptions.Compiled);

    public static string Create(string title)
    {
        return AlphabeticRegex.IsMatch(title)
            ? title
            : $"'{title}'";
    }
}