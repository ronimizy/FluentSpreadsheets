using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class RowHeightComponent : ComponentBase, IRowHeightComponent
{
    private readonly IComponent _component;

    public RowHeightComponent(IComponent component, int height)
    {
        _component = component;
        Height = height;
    }

    public override Size Size => _component.Size;

    public int Height { get; }

    public override void Accept(IComponentVisitor visitor)
    {
        visitor.Visit(this);
        _component.Accept(visitor);
    }
}