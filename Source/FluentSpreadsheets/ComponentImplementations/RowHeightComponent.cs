using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class RowHeightComponent : IRowHeightComponent
{
    private readonly IComponent _component;

    public RowHeightComponent(IComponent component, int height)
    {
        _component = component;
        Height = height;
    }

    public Size Size => _component.Size;

    public int Height { get; }

    public void Accept(IComponentVisitor visitor)
    {
        visitor.Visit(this);
        _component.Accept(visitor);
    }
}