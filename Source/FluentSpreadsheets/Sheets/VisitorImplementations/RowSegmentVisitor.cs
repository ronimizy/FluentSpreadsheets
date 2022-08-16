using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Sheets.Visitors;

namespace FluentSpreadsheets.Sheets;

internal class RowSegmentVisitor<THeaderData, TRowData> :
    IRowSegmentVisitor<TRowData>,
    IRowSegmentVisitor<HeaderRowData<THeaderData, TRowData>>
{
    private readonly THeaderData _headerData;
    private readonly IReadOnlyCollection<TRowData> _data;

    public RowSegmentVisitor(THeaderData headerData, IReadOnlyCollection<TRowData> data)
    {
        _headerData = headerData;
        _data = data;
        Components = Array.Empty<IComponent>();
    }

    public IList<IComponent> Components { get; private set; }

    public void Visit(IRowSegment<TRowData> builder)
    {
        Components = _data.Select(builder.BuildRow).ToArray();
    }

    public void Visit(IRowSegment<HeaderRowData<THeaderData, TRowData>> builder)
    {
        Components = _data
            .Select((x, i) => builder.BuildRow(new HeaderRowData<THeaderData, TRowData>(_headerData, x), i))
            .ToArray();
    }
}