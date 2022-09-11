namespace FluentSpreadsheets.Styles;

public readonly record struct FrameBorderStyle(
    BorderStyle? Leading,
    BorderStyle? Trailing,
    BorderStyle? Top,
    BorderStyle? Bottom) : IApplicable<FrameBorderStyle>
{
    public FrameBorderStyle Apply(FrameBorderStyle style)
    {
        return new FrameBorderStyle
        (
            Leading.TryApply(style.Leading),
            Trailing.TryApply(style.Trailing),
            Top.TryApply(style.Top),
            Bottom.TryApply(style.Bottom)
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