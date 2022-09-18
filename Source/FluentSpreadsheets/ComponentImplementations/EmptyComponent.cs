using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal sealed class EmptyComponent : ComponentBase, IComponent
{
    public EmptyComponent(Size size)
    {
        Size = size;
    }

    public override Size Size { get; }

    public override void Accept(IComponentVisitor visitor)
        => visitor.Visit(this);
}