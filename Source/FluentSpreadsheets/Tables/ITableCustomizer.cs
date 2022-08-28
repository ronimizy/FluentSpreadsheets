namespace FluentSpreadsheets.Tables;

public interface ITableCustomizer
{
    IComponent Customize(IComponent component);
}