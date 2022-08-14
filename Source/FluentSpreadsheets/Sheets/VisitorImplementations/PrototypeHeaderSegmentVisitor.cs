using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Sheets.Visitors;

namespace FluentSpreadsheets.Sheets;

internal class PrototypeHeaderSegmentVisitor<TData> : IHeaderSegmentVisitor<TData>
{
    private readonly IReadOnlyCollection<TData> _data;

    public PrototypeHeaderSegmentVisitor(IReadOnlyCollection<TData> data)
    {
        _data = data;
        Components = Array.Empty<IComponent>();
    }
    
    public IList<IComponent> Components { get; private set; }

    public void Visit(IHeaderSegment<TData> builder)
    {
        Components = _data.Select(builder.BuildHeader).ToArray();
    }
}