using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations.StylingComponents;

internal abstract class StylingComponentBase : IStylingComponent
{
    protected StylingComponentBase(IComponent component)
    {
        Component = component;
    }

    protected IComponent Component { get; }

    public Size Size => Component.Size;

    public abstract Style TryApply(Style style);

    public void Accept(IComponentVisitor visitor)
    {
        visitor.Visit(this);
        Component.Accept(visitor);
    }
}