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

    public void Accept(IComponentVisitor visitor)
        => _component.Value.Accept(visitor);

    protected abstract IComponent BuildBody();
}