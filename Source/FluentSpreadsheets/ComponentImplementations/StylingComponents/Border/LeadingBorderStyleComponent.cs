namespace FluentSpreadsheets.ComponentImplementations.StylingComponents.Border;

internal class LeadingBorderStyleComponent : BorderStyleComponent
{
    public LeadingBorderStyleComponent(IComponent component, BorderStyle borderStyle) 
        : base(component, borderStyle) { }

    protected override BorderStyle BorderStyleSelector(FrameBorderStyle frameBorderStyle)
        => frameBorderStyle.Leading;

    protected override FrameBorderStyle BorderStyleApplier(FrameBorderStyle frameBorderStyle, BorderStyle borderStyle)
    {
        return frameBorderStyle with
        {
            Leading = borderStyle,
        };
    }
}