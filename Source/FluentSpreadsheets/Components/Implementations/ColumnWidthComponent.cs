using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.Implementations;

internal sealed class ColumnWidthComponent : TopLevelComponentBase, IColumnWidthComponent
{
    public ColumnWidthComponent(IComponent component, RelativeSize width) : base(component)
    {
        Width = width;
    }

    public override Size Size => Wrapped.Size;

    public RelativeSize Width { get; }

    public override void Accept(IComponentVisitor visitor)
    {
        visitor.Visit(this);
        Wrapped.Accept(visitor);
    }

    protected override IComponent WrapIntoCurrent(IComponent component)
        => new ColumnWidthComponent(component, Width);
}