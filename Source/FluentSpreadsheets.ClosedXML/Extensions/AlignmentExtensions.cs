using ClosedXML.Excel;
using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.ClosedXML.Extensions;

internal static class AlignmentExtensions
{
    public static XLAlignmentVerticalValues ToXlAlignment(this VerticalAlignment alignment)
    {
        return alignment switch
        {
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
            HorizontalAlignment.Leading => XLAlignmentHorizontalValues.Left,
            HorizontalAlignment.Center => XLAlignmentHorizontalValues.Center,
            HorizontalAlignment.Trailing => XLAlignmentHorizontalValues.Right,
            _ => XLAlignmentHorizontalValues.Left,
        };
    }
}