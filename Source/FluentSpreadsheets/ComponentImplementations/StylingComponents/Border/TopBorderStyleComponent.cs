namespace FluentSpreadsheets.ComponentImplementations.StylingComponents.Border;

internal class TopBorderStyleComponent : BorderStyleComponent
{
    public TopBorderStyleComponent(IComponent component, BorderStyle borderStyle)
        : base(component, borderStyle) { }

    protected override BorderStyle BorderStyleSelector(FrameBorderStyle frameBorderStyle)
        => frameBorderStyle.Top;

    protected override FrameBorderStyle BorderStyleApplier(FrameBorderStyle frameBorderStyle, BorderStyle borderStyle)
    {
        return frameBorderStyle with
        {
            Leading = borderStyle,
        };
    }
}