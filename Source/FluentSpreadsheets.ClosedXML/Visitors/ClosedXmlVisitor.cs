using ClosedXML.Excel;
using FluentSpreadsheets.ClosedXML.Extensions;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ClosedXML.Visitors;

internal class ClosedXmlVisitor : ComponentVisitorBase
{
    private readonly IXLWorksheet _worksheet;

    public ClosedXmlVisitor(IXLWorksheet worksheet, Index index, Style style = default)
        : base(index, style)
    {
        _worksheet = worksheet;
    }

    public override Task FlushAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    protected override void StyleRange(Style style, IndexRange range)
    {
        var worksheetRange = _worksheet.Range(range);

        worksheetRange.Style.Alignment.Vertical = style.Alignment.Vertical.ToXlAlignment();
        worksheetRange.Style.Alignment.Horizontal = style.Alignment.Horizontal.ToXlAlignment();

        worksheetRange.Style.Border.ApplyBorderStyle(style.Border);
    }

    protected override void MergeRange(IndexRange range)
    {
        var worksheetRange = _worksheet.Range(range);
        worksheetRange.Merge();
    }

    protected override void WriteString(Index index, string value)
    {
        var worksheetCell = _worksheet.Cell(index);
        worksheetCell.Value = value;
    }

    protected override void AdjustRows(int from, int upTo)
    {
        _worksheet.Rows(from, upTo).AdjustToContents();
    }

    protected override void AdjustColumns(int from, int upTo)
    {
        _worksheet.Columns(from, upTo).AdjustToContents();
    }

    protected override void SetRowHeight(int from, int upTo, int height)
    {
        _worksheet.Rows(from, upTo - 1).Height = height;
    }

    protected override void SetColumnWidth(int from, int upTo, int width)
    {
        _worksheet.Columns(from, upTo - 1).Width = width;
    }
}