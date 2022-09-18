using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal sealed class ColumnWidthComponent : TopLevelComponentBase, IColumnWidthComponent
{
    public ColumnWidthComponent(IComponent component, int width) : base(component)
    {
        Width = width;
    }

    public override Size Size => Wrapped.Size;

    public int Width { get; }

    public override void Accept(IComponentVisitor visitor)
    {
        visitor.Visit(this);
        Wrapped.Accept(visitor);
    }

    protected override IComponent WrapIntoCurrent(IComponent component)
        => new ColumnWidthComponent(component, Width);
}