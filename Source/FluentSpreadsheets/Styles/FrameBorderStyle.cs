namespace FluentSpreadsheets.Styles;

public readonly record struct FrameBorderStyle(
    BorderStyle? Leading,
    BorderStyle? Trailing,
    BorderStyle? Top,
    BorderStyle? Bottom)
{
    public FrameBorderStyle Apply(FrameBorderStyle style)
    {
        return new FrameBorderStyle
        (
            style.Leading is null ? Leading : Leading?.Apply(style.Leading.Value) ?? style.Leading,
            style.Trailing is null ? Trailing : Trailing?.Apply(style.Trailing.Value) ?? style.Trailing,
            style.Top is null ? Top : Top?.Apply(style.Top.Value) ?? style.Top,
            style.Bottom is null ? Bottom : Bottom?.Apply(style.Bottom.Value) ?? style.Bottom
        );
    }

    public Style AsStyle()
    {
        return new Style
        {
            Border = this,
        };
    }
}