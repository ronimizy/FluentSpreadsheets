using System.Collections;

namespace FluentSpreadsheets;

internal class RowComponent : IRowComponent
{
    private readonly IEnumerable<IComponentSource> _enumerable;

    public RowComponent(IEnumerable<IComponentSource> enumerable)
    {
        _enumerable = enumerable;
    }

    public IEnumerator<IComponentSource> GetEnumerator()
        => _enumerable.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}