namespace FluentSpreadsheets.SheetSegments;

public abstract class SheetSegmentBase<THeaderData, TRowData, TFooterData> :
    ISheetSegment<THeaderData, TRowData, TFooterData>
{
    public IEnumerable<IComponent> BuildHeaders(THeaderData data)
        => Enumerable.Repeat(BuildHeader(data), 1);

    public IEnumerable<IComponent> BuildRows(HeaderRowData<THeaderData, TRowData> data, int rowIndex)
        => Enumerable.Repeat(BuildRow(data, rowIndex), 1);

    public IEnumerable<IComponent> BuildFooters(HeaderFooterData<THeaderData, TFooterData> data)
        => Enumerable.Repeat(BuildFooter(data), 1);

    protected abstract IComponent BuildHeader(THeaderData data);

    protected abstract IComponent BuildRow(HeaderRowData<THeaderData, TRowData> data, int rowIndex);

    protected virtual IComponent BuildFooter(HeaderFooterData<THeaderData, TFooterData> data)
        => ComponentFactory.None();
}