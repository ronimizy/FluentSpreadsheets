using System.Collections;

namespace FluentSpreadsheets.ComponentSourceImplementations;

internal class ForEachComponentSource<T> : IComponentSource
{
    private readonly IEnumerable<T> _data;
    private readonly Func<T, IComponentSource> _factory;

    public ForEachComponentSource(IEnumerable<T> data, Func<T, IComponentSource> factory)
    {
        _data = data;
        _factory = factory;
    }

    public IEnumerator<IComponentSource> GetEnumerator()
        => _data.Select(_factory).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}