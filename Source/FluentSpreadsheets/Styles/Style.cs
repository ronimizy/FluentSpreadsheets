using System.Drawing;
using FluentSpreadsheets.Styles.Text;

namespace FluentSpreadsheets.Styles;

public readonly record struct Style(
    FrameBorderStyle? Border,
    Alignment? Alignment,
    Color? Fill,
    TextStyle? Text) : IApplicable<Style>
{
    public Style Apply(Style style)
    {
        return new Style
        (
            Border.TryApply(style.Border),
            Alignment.TryApply(style.Alignment),
            style.Fill ?? Fill,
            Text.TryApply(style.Text)
        );
    }
}