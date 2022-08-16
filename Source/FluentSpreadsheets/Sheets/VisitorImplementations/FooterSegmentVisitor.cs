using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Sheets.Visitors;

namespace FluentSpreadsheets.Sheets;

internal class FooterSegmentVisitor<THeaderData, TFooterData> : 
    IFooterSegmentVisitor<TFooterData>,
    IFooterSegmentVisitor<HeaderFooterData<THeaderData, TFooterData>>
{
    private readonly THeaderData _headerData;
    private readonly TFooterData _data;

    public FooterSegmentVisitor(THeaderData headerData, TFooterData data)
    {
        _headerData = headerData;
        _data = data;
    }

    public IComponent? Component { get; private set; }

    public void Visit(IFooterSegment<TFooterData> builder)
    {
        Component = builder.BuildFooter(_data);
    }

    public void Visit(IFooterSegment<HeaderFooterData<THeaderData, TFooterData>> builder)
    {
        Component = builder.BuildFooter(new HeaderFooterData<THeaderData, TFooterData>(_headerData, _data));
    }
}