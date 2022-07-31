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

    public async Task AcceptAsync(IComponentVisitor visitor, CancellationToken cancellationToken)
    {
        await visitor.VisitAsync(this);
        await Component.AcceptAsync(visitor, cancellationToken);
    }
}