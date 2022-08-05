namespace FluentSpreadsheets;

public class InvalidStructureException : FluentSpreadsheetsException
{
    public InvalidStructureException(string message) : base(message) { }
}