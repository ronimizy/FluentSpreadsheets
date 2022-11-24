namespace FluentSpreadsheets;

public interface IColumnWidthComponent : IComponent
{
    public RelativeSize Width { get; }
}