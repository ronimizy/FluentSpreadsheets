using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Sheets.Visitors;

namespace FluentSpreadsheets.Sheets;

public abstract class RowSegmentBase<T> : IRowSegment<T>
{
    public abstract IComponent BuildRow(T data, int rowIndex);

    public void Accept(ISheetSegmentVisitor visitor)
    {
        if (visitor is IRowSegmentVisitor<T> rowBuilderVisitor)
            rowBuilderVisitor.Visit(this);
    }
}