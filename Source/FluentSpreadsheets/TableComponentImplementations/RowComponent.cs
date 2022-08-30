namespace FluentSpreadsheets.TableComponentImplementations;

internal class RowComponent : RowComponentBase
{
    private readonly IEnumerable<IBaseComponent> _enumerable;

    public RowComponent(IEnumerable<IBaseComponent> enumerable)
    {
        _enumerable = enumerable;
    }

    public override IEnumerator<IBaseComponent> GetEnumerator()
        => _enumerable.GetEnumerator();
}