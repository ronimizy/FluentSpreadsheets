using System.Drawing;
using ClosedXML.Excel;

namespace FluentSpreadsheets.ClosedXML.Extensions;

internal static class BorderExtensions
{
    public static void ApplyBorderStyle(this IXLBorder border, FrameBorderStyle style)
    {
        border.TopBorder = style.Top.Type.ToXlBorderStyle();
        border.TopBorderColor = style.Top.Color.ToXlColor();

        border.BottomBorder = style.Bottom.Type.ToXlBorderStyle();
        border.BottomBorderColor = style.Bottom.Color.ToXlColor();

        border.LeftBorder = style.Leading.Type.ToXlBorderStyle();
        border.LeftBorderColor = style.Leading.Color.ToXlColor();

        border.RightBorder = style.Trailing.Type.ToXlBorderStyle();
        border.RightBorderColor = style.Trailing.Color.ToXlColor();
    }

    public static XLBorderStyleValues ToXlBorderStyle(this BorderType type)
    {
        return type switch
        {
            BorderType.Unspecified => XLBorderStyleValues.None,
            BorderType.None => XLBorderStyleValues.None,
            BorderType.Dashed => XLBorderStyleValues.Dashed,
            BorderType.Dotted => XLBorderStyleValues.Dotted,
            BorderType.Double => XLBorderStyleValues.Double,
            BorderType.Medium => XLBorderStyleValues.Medium,
            BorderType.Thick => XLBorderStyleValues.Thick,
            BorderType.Thin => XLBorderStyleValues.Thin,
            _ => XLBorderStyleValues.None,
        };
    }

    public static XLColor ToXlColor(this Color? color)
        => color.HasValue ? XLColor.FromColor(color.Value) : XLColor.NoColor;
}