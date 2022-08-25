namespace FluentSpreadsheets.Styles;

public readonly record struct Style(FrameBorderStyle? Border, Alignment? Alignment)
{
    public Style Apply(Style style)
    {
        return new Style
        (
            style.Border is null ? Border : Border?.Apply(style.Border.Value) ?? style.Border,
            style.Alignment is null ? Alignment : Alignment?.Apply(style.Alignment.Value) ?? style.Alignment
        );
    }
}