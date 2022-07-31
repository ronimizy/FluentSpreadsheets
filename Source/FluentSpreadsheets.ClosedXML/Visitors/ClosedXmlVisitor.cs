using ClosedXML.Excel;
using FluentSpreadsheets.ClosedXML.Extensions;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ClosedXML.Visitors;

public class ClosedXmlVisitor : ComponentVisitorBase
{
    private readonly IXLWorksheet _worksheet;

    public ClosedXmlVisitor(IXLWorksheet worksheet, Index index, Style style = default)
        : base(index, style)
    {
        _worksheet = worksheet;
    }

    protected override Task StyleRangeAsync(Style style, IndexRange range, CancellationToken cancellationToken)
    {
        var worksheetRange = _worksheet.Range(range);

        worksheetRange.Style.Alignment.Vertical = style.Alignment.Vertical.ToXlAlignment();
        worksheetRange.Style.Alignment.Horizontal = style.Alignment.Horizontal.ToXlAlignment();

        worksheetRange.Style.Border.ApplyBorderStyle(style.Border);
        
        return Task.CompletedTask;
    }

    protected override Task MergeRangeAsync(IndexRange range, CancellationToken cancellationToken)
    {
        var worksheetRange = _worksheet.Range(range);
        worksheetRange.Merge();
        return Task.CompletedTask;
    }

    protected override Task WriteStringAsync(Index index, string value, CancellationToken cancellationToken)
    {
        var worksheetCell = _worksheet.Cell(index);
        worksheetCell.Value = value;
        return Task.CompletedTask;
    }

    protected override Task AdjustRowsAsync(int from, int upTo, CancellationToken cancellationToken)
    {
        _worksheet.Rows(from, upTo).AdjustToContents();
        return Task.CompletedTask;
    }

    protected override Task AdjustColumnsAsync(int from, int upTo, CancellationToken cancellationToken)
    {
        _worksheet.Columns(from, upTo).AdjustToContents();
        return Task.CompletedTask;
    }

    protected override Task SetRowHeightAsync(int from, int upTo, int height, CancellationToken cancellationToken)
    {
        _worksheet.Rows(from, upTo - 1).Height = height;
        return Task.CompletedTask;
    }

    protected override Task SetColumnWidthAsync(int from, int upTo, int width, CancellationToken cancellationToken)
    {
        _worksheet.Columns(from, upTo - 1).Width = width;
        return Task.CompletedTask;
    }
}