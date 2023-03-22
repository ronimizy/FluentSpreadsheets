using System.Drawing;
using ClosedXML.Excel;
using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Styles.Text;

namespace FluentSpreadsheets.ClosedXML.Extensions;

internal static class XlRangeExtensions
{
    public static void ApplyStyle(this IXLRange range, Style style)
    {
        range.ApplyAlignment(style.Alignment);
        range.ApplyBorderStyle(style.Border);
        range.ApplyFillStyle(style.Fill);
        range.ApplyTextStyle(style.Text);
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

    private static void ApplyTextStyle(this IXLRange range, TextStyle? textStyle)
    {
        if (textStyle is null)
            return;

        if (textStyle.Value.Color is not null)
            range.Style.Font.FontColor = XLColor.FromColor(textStyle.Value.Color.Value);

        if (textStyle.Value.Kind is not null)
        {
            var kind = textStyle.Value.Kind.Value;

            range.Style.Font.Italic = kind.HasFlag(TextKind.Italic);
            range.Style.Font.Bold = kind.HasFlag(TextKind.Bold);
        }

        if (textStyle.Value.Wrapping is TextWrapping.Wrap)
        {
            range.Style.Alignment.WrapText = true;
        }
    }
}