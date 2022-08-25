using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.GoogleSheets.Extensions;

internal static class AlignmentExtensions
{
    public static string ToGoogleSheetsAlignment(this VerticalAlignment alignment)
    {
        return alignment switch
        {
            VerticalAlignment.Top => "TOP",
            VerticalAlignment.Center => "MIDDLE",
            VerticalAlignment.Bottom => "BOTTOM",
            _ => "VERTICAL_ALIGN_UNSPECIFIED",
        };
    }
    
    public static string ToGoogleSheetsAlignment(this HorizontalAlignment alignment)
    {
        return alignment switch
        {
            HorizontalAlignment.Leading => "LEFT",
            HorizontalAlignment.Center => "CENTER",
            HorizontalAlignment.Trailing => "RIGHT",
            _ => "HORIZONTAL_ALIGN_UNSPECIFIED",
        };
    }
}