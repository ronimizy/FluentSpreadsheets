using ClosedXML.Excel;

namespace FluentSpreadsheets.ClosedXML.Extensions;

internal static class AlignmentExtensions
{
    public static XLAlignmentVerticalValues ToXlAlignment(this VerticalAlignment alignment)
    {
        return alignment switch
        {
            VerticalAlignment.Unspecified => XLAlignmentVerticalValues.Bottom,
            VerticalAlignment.Top => XLAlignmentVerticalValues.Top,
            VerticalAlignment.Center => XLAlignmentVerticalValues.Center,
            VerticalAlignment.Bottom => XLAlignmentVerticalValues.Bottom,
            _ => XLAlignmentVerticalValues.Bottom,
        };
    }
    
    public static XLAlignmentHorizontalValues ToXlAlignment(this HorizontalAlignment alignment)
    {
        return alignment switch
        {
            HorizontalAlignment.Unspecified => XLAlignmentHorizontalValues.Left,
            HorizontalAlignment.Leading => XLAlignmentHorizontalValues.Left,
            HorizontalAlignment.Center => XLAlignmentHorizontalValues.Center,
            HorizontalAlignment.Trailing => XLAlignmentHorizontalValues.Right,
            _ => XLAlignmentHorizontalValues.Left,
        };
    }
}