using FluentSpreadsheets.Sheets.Visitors;

namespace FluentSpreadsheets.Sheets.Segments;

public interface ISheetSegment
{
    void Accept(ISheetSegmentVisitor visitor);
}