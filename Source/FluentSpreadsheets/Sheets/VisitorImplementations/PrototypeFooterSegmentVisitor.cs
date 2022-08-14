using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Sheets.Visitors;

namespace FluentSpreadsheets.Sheets;

internal class PrototypeFooterSegmentVisitor<THeaderData, TFooterData> :
    IFooterSegmentVisitor<TFooterData>,
    IFooterSegmentVisitor<HeaderFooterData<THeaderData, TFooterData>>
{
    private readonly IReadOnlyCollection<THeaderData> _headerData;
    private readonly TFooterData _footerData;

    public PrototypeFooterSegmentVisitor(IReadOnlyCollection<THeaderData> headerData, TFooterData footerData)
    {
        _headerData = headerData;
        _footerData = footerData;

        Components = Array.Empty<IComponent>();
    }

    public IList<IComponent> Components { get; private set; }

    public void Visit(IFooterSegment<TFooterData> builder)
    {
        Components = _headerData.Select(_ => builder.BuildFooter(_footerData)).ToArray();
    }

    public void Visit(IFooterSegment<HeaderFooterData<THeaderData, TFooterData>> builder)
    {
        Components = _headerData
            .Select(x => builder.BuildFooter(new HeaderFooterData<THeaderData, TFooterData>(x, _footerData)))
            .ToArray();
    }
}