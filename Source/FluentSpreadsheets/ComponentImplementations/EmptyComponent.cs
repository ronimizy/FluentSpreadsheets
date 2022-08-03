using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

public class EmptyComponent : IComponent
{
    public EmptyComponent(Size size)
    {
        Size = size;
    }

    public Size Size { get; }

    public void Accept(IComponentVisitor visitor)
        => visitor.Visit(this);
}