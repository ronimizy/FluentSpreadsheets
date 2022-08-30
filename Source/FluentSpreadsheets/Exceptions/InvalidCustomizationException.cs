namespace FluentSpreadsheets;

public class InvalidCustomizationException : FluentSpreadsheetsException
{
    public InvalidCustomizationException(string message) : base(message) { }
}