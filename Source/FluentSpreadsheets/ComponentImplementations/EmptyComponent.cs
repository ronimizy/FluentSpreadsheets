using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

public class EmptyComponent : IComponent
{
    public EmptyComponent(Size size)
    {
        Size = size;
    }

    public Size Size { get; }

    public Task AcceptAsync(IComponentVisitor visitor, CancellationToken cancellationToken)
        => visitor.VisitAsync(this, cancellationToken);
}