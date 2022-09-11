using System.Drawing;
using GoogleColor = Google.Apis.Sheets.v4.Data.Color;

namespace FluentSpreadsheets.GoogleSheets.Extensions;

internal static class ColorExtensions
{
    public static GoogleColor ToGoogleColor(this Color color)
    {
        return new GoogleColor
        {
            Red = color.R / 255f,
            Green = color.G / 255f,
            Blue = color.B / 255f,
            Alpha = color.A / 255f,
        };
    }
}