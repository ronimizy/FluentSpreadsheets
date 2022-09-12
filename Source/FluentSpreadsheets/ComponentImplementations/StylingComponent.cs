using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class StylingComponent : TopLevelComponentBase, IStylingComponent
{
    public StylingComponent(IComponent styledComponent, Style style) : base(styledComponent)
    {
        StyledComponent = styledComponent;
        Style = style;
    }

    public override Size Size => StyledComponent.Size;

    public IComponent StyledComponent { get; }

    public Style Style { get; }

    public override void Accept(IComponentVisitor visitor)
    {
        visitor.Visit(this);
        StyledComponent.Accept(visitor);
    }

    protected override IComponent WrapIntoCurrent(IComponent component)
        => new StylingComponent(component, Style);
}