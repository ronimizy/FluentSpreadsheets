using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Sheets.Visitors;

namespace FluentSpreadsheets.Sheets;

public abstract class HeaderRowSegmentBase<THeaderData, TRowData> : IHeaderRowSegment<THeaderData, TRowData>
{
    public abstract IComponent BuildHeader(THeaderData data);

    public abstract IComponent BuildRow(TRowData data, int rowIndex);

    public virtual void Accept(ISheetSegmentVisitor visitor)
    {
        if (visitor is IHeaderSegmentVisitor<THeaderData> headerBuilderVisitor)
            headerBuilderVisitor.Visit(this);
        
        if (visitor is IRowSegmentVisitor<TRowData> rowBuilderVisitor)
            rowBuilderVisitor.Visit(this);
    }
}