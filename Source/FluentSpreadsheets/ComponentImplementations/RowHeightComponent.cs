using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class RowHeightComponent : TopLevelComponentBase, IRowHeightComponent
{
    public RowHeightComponent(IComponent component, int height) : base(component)
    {
        Height = height;
    }

    public override Size Size => Wrapped.Size;

    public int Height { get; }

    public override void Accept(IComponentVisitor visitor)
    {
        visitor.Visit(this);
        Wrapped.Accept(visitor);
    }

    protected override IComponent WrapIntoCurrent(IComponent component)
        => new RowHeightComponent(component, Height);
}