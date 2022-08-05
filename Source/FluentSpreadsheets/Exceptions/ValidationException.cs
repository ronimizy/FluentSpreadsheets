namespace FluentSpreadsheets;

public class ValidationException : FluentSpreadsheetsException
{
    public ValidationException(string message) : base(message) { }
}