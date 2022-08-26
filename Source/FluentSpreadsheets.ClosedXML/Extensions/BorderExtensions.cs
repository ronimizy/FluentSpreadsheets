using System.Drawing;
using ClosedXML.Excel;
using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.ClosedXML.Extensions;

internal static class BorderExtensions
{
    public static void ApplyBorderStyle(this IXLBorder border, FrameBorderStyle style)
    {
        border.TopBorder = style.Top?.Type?.ToXlBorderStyle() ?? border.TopBorder;
        border.TopBorderColor = style.Top?.Color.ToXlColor() ?? border.TopBorderColor;

        border.BottomBorder = style.Bottom?.Type?.ToXlBorderStyle() ?? border.BottomBorder;
        border.BottomBorderColor = style.Bottom?.Color.ToXlColor() ?? border.BottomBorderColor;
        
        border.LeftBorder = style.Leading?.Type?.ToXlBorderStyle() ?? border.LeftBorder;
        border.LeftBorderColor = style.Leading?.Color.ToXlColor() ?? border.LeftBorderColor;
        
        border.RightBorder = style.Trailing?.Type?.ToXlBorderStyle() ?? border.RightBorder;
        border.RightBorderColor = style.Trailing?.Color.ToXlColor() ?? border.RightBorderColor;
    }

    public static XLBorderStyleValues ToXlBorderStyle(this BorderType type)
    {
        return type switch
        {
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