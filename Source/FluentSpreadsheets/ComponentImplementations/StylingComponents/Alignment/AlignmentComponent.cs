namespace FluentSpreadsheets.ComponentImplementations.StylingComponents.Alignment;

internal class AlignmentComponent : StylingComponentBase
{
    private readonly HorizontalAlignment _horizontalAlignment;
    private readonly VerticalAlignment _verticalAlignment;

    public AlignmentComponent(
        IComponent component,
        HorizontalAlignment horizontalAlignment,
        VerticalAlignment verticalAlignment) : base(component)
    {
        _horizontalAlignment = horizontalAlignment;
        _verticalAlignment = verticalAlignment;
    }

    public override Style TryApply(Style style)
    {
        if (style.Alignment.Horizontal is HorizontalAlignment.Unspecified)
        {
            style = style with
            {
                Alignment = style.Alignment with
                {
                    Horizontal = _horizontalAlignment
                }
            };
        }
        
        if (style.Alignment.Vertical is VerticalAlignment.Unspecified)
        {
            style = style with
            {
                Alignment = style.Alignment with
                {
                    Vertical = _verticalAlignment
                }
            };
        }
        
        return style;
    }
}