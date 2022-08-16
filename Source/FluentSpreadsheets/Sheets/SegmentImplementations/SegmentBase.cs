using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Sheets.Visitors;

namespace FluentSpreadsheets.Sheets;

public abstract class SegmentBase<THeaderData, TRowData, TFooterData> :
    IHeaderRowFooterSegment<THeaderData, TRowData, TFooterData>
{
    public abstract IComponent BuildHeader(THeaderData data);

    public abstract IComponent BuildRow(TRowData data, int rowIndex);

    public abstract IComponent BuildFooter(TFooterData data);

    public virtual void Accept(ISheetSegmentVisitor visitor)
    {
        if (visitor is IHeaderSegmentVisitor<THeaderData> headerBuilderVisitor)
            headerBuilderVisitor.Visit(this);
        
        if (visitor is IRowSegmentVisitor<TRowData> rowBuilderVisitor)
            rowBuilderVisitor.Visit(this);
        
        if (visitor is IFooterSegmentVisitor<TFooterData> footerBuilderVisitor)
            footerBuilderVisitor.Visit(this);
    }
}