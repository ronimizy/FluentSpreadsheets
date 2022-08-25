using FluentSpreadsheets.Styles;
using Google.Apis.Sheets.v4.Data;
using SystemColor = System.Drawing.Color;

namespace FluentSpreadsheets.GoogleSheets.Extensions;

internal static class BorderExtensions
{
    public static Border? ToGoogleSheetsBorder(this BorderStyle borderStyle)
    {
        var sheetsBorderStyle = GetGoogleSheetsBorderStyle(borderStyle.Type);

        if (sheetsBorderStyle is null)
            return null;

        return new Border
        {
            Style = sheetsBorderStyle,
            ColorStyle = new ColorStyle
            {
                RgbColor = GetGoogleSheetsColor(borderStyle.Color),
            },
        };
    }

    private static string? GetGoogleSheetsBorderStyle(BorderType? type)
    {
        return type switch
        {
            BorderType.None => "NONE",
            BorderType.Dashed => "DASHED",
            BorderType.Dotted => "DOTTED",
            BorderType.Double => "DOUBLE",
            BorderType.Medium => "SOLID_MEDIUM",
            BorderType.Thick => "SOLID_THICK",
            BorderType.Thin => "SOLID",
            _ => null,
        };
    }

    private static Color GetGoogleSheetsColor(SystemColor? color)
    {
        if (!color.HasValue)
            return new Color();

        return new Color
        {
            Alpha = color.Value.A,
            Red = color.Value.R,
            Green = color.Value.G,
            Blue = color.Value.B,
        };
    }
}