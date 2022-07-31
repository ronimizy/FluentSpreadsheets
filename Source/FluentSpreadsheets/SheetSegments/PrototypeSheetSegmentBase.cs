namespace FluentSpreadsheets.SheetSegments;

public abstract class PrototypeSheetSegmentBase<
    THeaderData,
    TRowData,
    TFooterData,
    TPrototypeHeaderData,
    TPrototypeRowData,
    TPrototypeFooterData> :
    ISheetSegment<THeaderData, TRowData, TFooterData>
{
    public IEnumerable<IComponent> BuildHeaders(THeaderData data)
        => SelectHeaderData(data).Select(BuildHeader);

    public IEnumerable<IComponent> BuildRows(HeaderRowData<THeaderData, TRowData> data, int rowIndex)
    {
        return SelectHeaderData(data.HeaderData)
            .Select(x => new HeaderRowData<TPrototypeHeaderData, TRowData>(x, data.RowData))
            .Select(x => new HeaderRowData<TPrototypeHeaderData, TPrototypeRowData>(x.HeaderData, SelectRowData(x)))
            .Select(x => BuildRow(x, rowIndex));
    }

    public IEnumerable<IComponent> BuildFooters(HeaderFooterData<THeaderData, TFooterData> data)
    {
        return SelectHeaderData(data.HeaderData)
            .Select(x => new HeaderFooterData<TPrototypeHeaderData, TFooterData>(x, data.FooterData))
            .Select(x => new HeaderFooterData<TPrototypeHeaderData, TPrototypeFooterData>(x.HeaderData,
                SelectFooterData(x)))
            .Select(BuildFooter);
    }

    protected abstract IComponent BuildHeader(TPrototypeHeaderData data);

    protected abstract IComponent BuildRow(HeaderRowData<TPrototypeHeaderData, TPrototypeRowData> data, int rowIndex);

    protected virtual IComponent BuildFooter(HeaderFooterData<TPrototypeHeaderData, TPrototypeFooterData> data)
        => ComponentFactory.None();

    protected abstract IEnumerable<TPrototypeHeaderData> SelectHeaderData(THeaderData data);

    protected abstract TPrototypeRowData SelectRowData(HeaderRowData<TPrototypeHeaderData, TRowData> data);

    protected abstract TPrototypeFooterData SelectFooterData(HeaderFooterData<TPrototypeHeaderData, TFooterData> data);
}

public abstract class PrototypeSheetSegmentBase<THeaderData, TRowData, TFooterData, TPrototypeHeaderData> :
    PrototypeSheetSegmentBase<THeaderData, TRowData, TFooterData, TPrototypeHeaderData, TRowData, TFooterData>
{
    protected override TRowData SelectRowData(HeaderRowData<TPrototypeHeaderData, TRowData> data)
        => data.RowData;

    protected override TFooterData SelectFooterData(HeaderFooterData<TPrototypeHeaderData, TFooterData> data)
        => data.FooterData;
}

public abstract class PrototypeSheetSegmentBase<
    THeaderData,
    TRowData,
    TFooterData,
    TPrototypeHeaderData,
    TPrototypeRowData> :
    PrototypeSheetSegmentBase<THeaderData, TRowData, TFooterData, TPrototypeHeaderData, TPrototypeRowData, TFooterData>
{
    protected override TFooterData SelectFooterData(HeaderFooterData<TPrototypeHeaderData, TFooterData> data)
        => data.FooterData;
}