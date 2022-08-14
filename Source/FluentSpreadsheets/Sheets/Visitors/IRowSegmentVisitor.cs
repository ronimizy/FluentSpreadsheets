using FluentSpreadsheets.Sheets.Segments;

namespace FluentSpreadsheets.Sheets.Visitors;

public interface IRowSegmentVisitor<out TData> : ISheetSegmentVisitor
{
    void Visit(IRowSegment<TData> builder);
}