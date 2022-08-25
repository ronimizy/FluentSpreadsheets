using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

public class StylingComponent : IStylingComponent
{
    public StylingComponent(IComponent styledComponent, Style style)
    {
        StyledComponent = styledComponent;
        Style = style;
    }

    public Size Size => StyledComponent.Size;

    public IComponent StyledComponent { get; }
    
    public Style Style { get; }

    public void Accept(IComponentVisitor visitor)
    {
        visitor.Visit(this);
        StyledComponent.Accept(visitor);
    }
}