namespace FluentSpreadsheets.Tables;

internal sealed class DynamicTable<T> : RowTable<T>
{
    private readonly Func<T, IEnumerable<IRowComponent>> _generator;

    public DynamicTable(Func<T, IEnumerable<IRowComponent>> generator)
    {
        _generator = generator;
    }

    protected override IEnumerable<IRowComponent> RenderRows(T model)
        => _generator.Invoke(model);
}