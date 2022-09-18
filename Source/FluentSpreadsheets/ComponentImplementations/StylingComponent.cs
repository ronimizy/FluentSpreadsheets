using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal sealed class StylingComponent : TopLevelComponentBase, IStylingComponent
{
    public StylingComponent(IComponent styledComponent, Style style)
        : base(styledComponent, styledComponent.Style.Apply(style))
    {
        StyledComponent = styledComponent;
    }

    public override Size Size => StyledComponent.Size;

    public IComponent StyledComponent { get; }

    public override void Accept(IComponentVisitor visitor)
    {
        visitor.Visit(this);
        StyledComponent.Accept(visitor);
    }

    public override IComponent WithStyleApplied(Style style)
    {
        var newStyle = Style.Apply(style);
        return new StylingComponent(StyledComponent, newStyle);
    }

    protected override IComponent WrapIntoCurrent(IComponent component)
        => new StylingComponent(component, Style);
}