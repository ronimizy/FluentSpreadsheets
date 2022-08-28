using System.Collections;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets;

public abstract class CustomComponent : IComponent
{
    private readonly IEnumerable<IComponent> _components;
    private readonly Lazy<IComponent> _component;

    protected CustomComponent()
    {
        _component = new Lazy<IComponent>(BuildBody);
        _components = Enumerable.Repeat(this, 1);
    }

    public Size Size => _component.Value.Size;

    public void Accept(IComponentVisitor visitor)
        => _component.Value.Accept(visitor);

    public IEnumerator<IComponentSource> GetEnumerator()
        => _components.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    protected abstract IComponent BuildBody();
}