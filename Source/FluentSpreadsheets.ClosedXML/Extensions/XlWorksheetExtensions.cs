using ClosedXML.Excel;
using Index = FluentSpreadsheets.Index;

namespace FluentSpreadsheets.ClosedXML.Extensions;

internal static class XlWorksheetExtensions
{
    public static IXLRange Range(this IXLWorksheet worksheet, IndexRange range)
    {
        return worksheet.Range(
            range.Start.Row,
            range.Start.Column,
            range.End.Row - 1,
            range.End.Column - 1);
    }

    public static IXLCell Cell(this IXLWorksheet worksheet, Index index)
        => worksheet.Cell(index.Row, index.Column);
}