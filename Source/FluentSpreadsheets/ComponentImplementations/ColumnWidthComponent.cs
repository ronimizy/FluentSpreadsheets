using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class ColumnWidthComponent : IColumnWidthComponent
{
    private readonly IComponent _component;

    public ColumnWidthComponent(IComponent component, int width)
    {
        _component = component;
        Width = width;
    }

    public Size Size => _component.Size;

    public int Width { get; }

    public void Accept(IComponentVisitor visitor)
    {
        visitor.Visit(this);
        _component.Accept(visitor);
    }
}