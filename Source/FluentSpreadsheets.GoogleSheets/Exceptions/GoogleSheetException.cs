namespace FluentSpreadsheets.GoogleSheets.Exceptions;

public class GoogleSheetException : Exception
{
    public GoogleSheetException(string message) : base(message) { }
}