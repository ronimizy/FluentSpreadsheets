namespace FluentSpreadsheets.ComponentImplementations.StylingComponents.Border;

internal abstract class BorderStyleComponent : StylingComponentBase
{
    private readonly BorderStyle _borderStyle;

    protected BorderStyleComponent(IComponent component, BorderStyle borderStyle) : base(component)
    {
        _borderStyle = borderStyle;
    }

    public override Style TryApply(Style style)
    {
        var selectedStyle = BorderStyleSelector(style.Border);

        if (selectedStyle.Type is BorderType.Unspecified)
        {
            selectedStyle = selectedStyle with
            {
                Type = _borderStyle.Type,
            };

            style = style with
            {
                Border = BorderStyleApplier(style.Border, selectedStyle),
            };
        }

        if (selectedStyle.Color is null)
        {
            selectedStyle = selectedStyle with
            {
                Color = _borderStyle.Color,
            };

            style = style with
            {
                Border = BorderStyleApplier(style.Border, selectedStyle),
            };
        }

        return style;
    }

    protected abstract BorderStyle BorderStyleSelector(FrameBorderStyle frameBorderStyle);

    protected abstract FrameBorderStyle BorderStyleApplier(FrameBorderStyle frameBorderStyle, BorderStyle borderStyle);
}