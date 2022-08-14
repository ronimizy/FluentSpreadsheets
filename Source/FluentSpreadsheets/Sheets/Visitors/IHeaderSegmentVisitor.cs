using FluentSpreadsheets.Sheets.Segments;

namespace FluentSpreadsheets.Sheets.Visitors;

public interface IHeaderSegmentVisitor<out TData> : ISheetSegmentVisitor
{
    void Visit(IHeaderSegment<TData> builder);
}