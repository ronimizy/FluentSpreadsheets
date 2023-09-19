namespace FluentSpreadsheets;

public class UnsetLabelException : FluentSpreadsheetsException
{
    public UnsetLabelException(string message) : base(message) { }
}