using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets;

public abstract class ComponentBase : IComponent
{
    private readonly Lazy<IComponent> _component;

    protected ComponentBase()
    {
        _component = new Lazy<IComponent>(BuildBody);
    }

    public Size Size => _component.Value.Size;

    public Task AcceptAsync(IComponentVisitor visitor, CancellationToken cancellationToken = default)
        => _component.Value.AcceptAsync(visitor, cancellationToken);

    protected abstract IComponent BuildBody();
}