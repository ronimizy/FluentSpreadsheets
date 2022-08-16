using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Sheets.Visitors;

namespace FluentSpreadsheets.Sheets;

internal class PrototypeRowSegmentVisitor<THeaderData, TRowData> :
    IRowSegmentVisitor<TRowData>,
    IRowSegmentVisitor<HeaderRowData<THeaderData, TRowData>>
{
    private readonly IReadOnlyCollection<THeaderData> _headerData;
    private readonly IReadOnlyCollection<TRowData> _rowData;

    public PrototypeRowSegmentVisitor(
        IReadOnlyCollection<THeaderData> headerData,
        IReadOnlyCollection<TRowData> rowData)
    {
        _headerData = headerData;
        _rowData = rowData;

        Components = Array.Empty<IList<IComponent>>();
    }

    public IList<IList<IComponent>> Components { get; private set; }

    public void Visit(IRowSegment<TRowData> builder)
    {
        Components = _headerData
            .Select(_ => (IList<IComponent>)_rowData.Select(builder.BuildRow).ToArray())
            .ToArray();
    }

    public void Visit(IRowSegment<HeaderRowData<THeaderData, TRowData>> builder)
    {
        Components = _headerData
            .Select(x => BuildRows(x, builder))
            .ToArray();
    }

    private IList<IComponent> BuildRows<TDestination>(
        TDestination destination,
        IRowSegment<HeaderRowData<TDestination, TRowData>> row)
    {
        return _rowData
            .Select((x, i) => row.BuildRow(new HeaderRowData<TDestination, TRowData>(destination, x), i))
            .ToArray();
    }
}