using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class StylingComponent : ComponentBase, IStylingComponent
{
    public StylingComponent(IComponent styledComponent, Style style)
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
}