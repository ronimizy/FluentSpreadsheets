namespace FluentSpreadsheets.ComponentImplementations.StylingComponents.Border;

internal class TrailingBorderStyleComponent : BorderStyleComponent
{
    public TrailingBorderStyleComponent(IComponent component, BorderStyle borderStyle)
        : base(component, borderStyle) { }

    protected override BorderStyle BorderStyleSelector(FrameBorderStyle frameBorderStyle)
        => frameBorderStyle.Trailing;

    protected override FrameBorderStyle BorderStyleApplier(FrameBorderStyle frameBorderStyle, BorderStyle borderStyle)
    {
        return frameBorderStyle with
        {
            Trailing = borderStyle,
        };
    }
}