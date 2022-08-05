namespace FluentSpreadsheets;

public class FluentSpreadsheetsException : Exception
{
    public FluentSpreadsheetsException() { }
    public FluentSpreadsheetsException(string message) : base(message) { }
    public FluentSpreadsheetsException(string message, Exception innerException) : base(message, innerException) { }
}