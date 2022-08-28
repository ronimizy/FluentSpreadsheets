namespace FluentSpreadsheets.ComponentSourceImplementations;

internal class ForEachComponentSource<T> : ComponentSourceBase
{
    private readonly IEnumerable<T> _data;
    private readonly Func<T, IBaseComponent> _factory;

    public ForEachComponentSource(IEnumerable<T> data, Func<T, IBaseComponent> factory)
    {
        _data = data;
        _factory = factory;
    }

    public override IEnumerator<IBaseComponent> GetEnumerator()
        => _data.Select(_factory).GetEnumerator();
}