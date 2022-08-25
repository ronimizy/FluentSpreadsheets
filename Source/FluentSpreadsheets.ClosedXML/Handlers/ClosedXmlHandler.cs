using ClosedXML.Excel;
using FluentSpreadsheets.ClosedXML.Extensions;
using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ClosedXML.Handlers;

internal readonly struct ClosedXmlHandler : IComponentVisitorHandler
{
    private readonly IXLWorksheet _worksheet;

    public ClosedXmlHandler(IXLWorksheet worksheet)
    {
        _worksheet = worksheet;
    }

    public void StyleRange(Style style, IndexRange range)
    {
        var worksheetRange = _worksheet.Range(range);

        if (style.Alignment is not null)
        {
            XLAlignmentVerticalValues? vertical = style.Alignment.Value.Vertical?.ToXlAlignment();
            XLAlignmentHorizontalValues? horizontal = style.Alignment.Value.Horizontal?.ToXlAlignment();

            if (vertical is not null)
                worksheetRange.Style.Alignment.Vertical = vertical.Value;

            if (horizontal is not null)
                worksheetRange.Style.Alignment.Horizontal = horizontal.Value;
        }

        if (style.Border is not null)
            worksheetRange.Style.Border.ApplyBorderStyle(style.Border.Value);
    }

    public void MergeRange(IndexRange range)
    {
        var worksheetRange = _worksheet.Range(range);
        worksheetRange.Merge();
    }

    public void WriteString(Index index, string value)
    {
        var worksheetCell = _worksheet.Cell(index);
        worksheetCell.Value = value;
    }

    public void AdjustRows(int from, int upTo)
    {
        _worksheet.Rows(from, upTo).AdjustToContents();
    }

    public void AdjustColumns(int from, int upTo)
    {
        _worksheet.Columns(from, upTo).AdjustToContents();
    }

    public void SetRowHeight(int from, int upTo, int height)
    {
        _worksheet.Rows(from, upTo - 1).Height = height;
    }

    public void SetColumnWidth(int from, int upTo, int width)
    {
        _worksheet.Columns(from, upTo - 1).Width = width;
    }
}