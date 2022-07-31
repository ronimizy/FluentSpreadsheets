namespace FluentSpreadsheets.ComponentImplementations.StylingComponents.Border;

internal class BottomBorderStyleComponent : BorderStyleComponent
{
    public BottomBorderStyleComponent(IComponent component, BorderStyle borderStyle)
        : base(component, borderStyle) { }

    protected override BorderStyle BorderStyleSelector(FrameBorderStyle frameBorderStyle)
        => frameBorderStyle.Bottom;

    protected override FrameBorderStyle BorderStyleApplier(FrameBorderStyle frameBorderStyle, BorderStyle borderStyle)
    {
        return frameBorderStyle with
        {
            Bottom = borderStyle,
        };
    }
}