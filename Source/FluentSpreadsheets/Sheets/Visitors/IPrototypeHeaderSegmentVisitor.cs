using FluentSpreadsheets.Sheets.Segments;

namespace FluentSpreadsheets.Sheets.Visitors;

public interface IPrototypeHeaderSegmentVisitor<out TSource> : ISheetSegmentVisitor
{
    void Visit<TDestination>(IPrototypeHeaderSegment<TSource, TDestination> builder);
}