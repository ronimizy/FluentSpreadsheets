namespace FluentSpreadsheets.Tables;

public interface ITableCustomizer
{
    IComponentSource Customize(IComponent component);
}