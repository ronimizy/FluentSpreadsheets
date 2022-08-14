using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Sheets.Visitors;

namespace FluentSpreadsheets.Sheets;

internal class HeaderSegmentVisitor<TData> : IHeaderSegmentVisitor<TData>
{
    private readonly TData _data;

    public HeaderSegmentVisitor(TData data)
    {
        _data = data;
    }

    public IComponent? Component { get; private set; }

    public void Visit(IHeaderSegment<TData> builder)
    {
        Component = builder.BuildHeader(_data);
    }
}