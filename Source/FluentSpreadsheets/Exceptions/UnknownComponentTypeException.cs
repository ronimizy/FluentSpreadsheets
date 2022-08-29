namespace FluentSpreadsheets;

public class UnknownComponentTypeException : FluentSpreadsheetsException
{
    public UnknownComponentTypeException(IBaseComponent component)
        : base($"Component of type {component.GetType()} is not recognized by the system") { }
}