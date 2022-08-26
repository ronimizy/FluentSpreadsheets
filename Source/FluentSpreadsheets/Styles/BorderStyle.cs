using System.Drawing;

namespace FluentSpreadsheets.Styles;

public readonly record struct BorderStyle(BorderType? Type, Color? Color)
{
    public BorderStyle Apply(BorderStyle style)
    {
        return new BorderStyle
        (
            style.Type ?? Type,
            style.Color ?? Color
        );
    }
}