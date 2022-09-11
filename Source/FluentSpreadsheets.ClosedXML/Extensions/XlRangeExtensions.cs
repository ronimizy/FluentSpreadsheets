using System.Drawing;
using ClosedXML.Excel;
using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.ClosedXML.Extensions;

internal static class XlRangeExtensions
{
    public static void ApplyStyle(this IXLRange range, Style style)
    {
        range.ApplyAlignment(style.Alignment);
        range.ApplyBorderStyle(style.Border);
        range.ApplyFillStyle(style.Fill);
    }

    private static void ApplyAlignment(this IXLRange range, Alignment? alignment)
    {
        if (alignment is null)
            return;

        XLAlignmentVerticalValues? vertical = alignment.Value.Vertical?.ToXlAlignment();
        XLAlignmentHorizontalValues? horizontal = alignment.Value.Horizontal?.ToXlAlignment();

        if (vertical is not null)
            range.Style.Alignment.Vertical = vertical.Value;

        if (horizontal is not null)
            range.Style.Alignment.Horizontal = horizontal.Value;
    }

    private static void ApplyBorderStyle(this IXLRange range, FrameBorderStyle? border)
    {
        if (border is not null)
            range.Style.Border.ApplyBorderStyle(border.Value);
    }

    private static void ApplyFillStyle(this IXLRange range, Color? fill)
    {
        if (fill is not null)
            range.Style.Fill.BackgroundColor = XLColor.FromColor(fill.Value);
    }
}