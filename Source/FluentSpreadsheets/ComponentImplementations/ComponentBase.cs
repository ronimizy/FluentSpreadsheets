using System.Collections;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal abstract class ComponentBase : IComponent
{
    private readonly IEnumerable<IComponent> _components;

    protected ComponentBase()
    {
        _components = Enumerable.Repeat(this, 1);
    }

    public abstract Size Size { get; }

    public abstract void Accept(IComponentVisitor visitor);

    public IEnumerator<IComponentSource> GetEnumerator()
        => _components.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}