namespace FluentSpreadsheets;

public class InvalidCustomizationException : FluentSpreadsheetsException
{
    public InvalidCustomizationException()
        : base("Customized component height must be equal to the component height.") { }
}