using FluentSpreadsheets.Sheets.Segments;

namespace FluentSpreadsheets.Sheets.Visitors;

public interface IFooterSegmentVisitor<out TData> : ISheetSegmentVisitor
{
    void Visit(IFooterSegment<TData> builder);
}