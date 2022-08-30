namespace FluentSpreadsheets.Tables;

public interface ITable { }

public interface ITable<in T> : ITable
{
    IComponent Render(T model);
}