using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets;

public abstract class CustomComponent : IComponent
{
    private readonly Lazy<IComponent> _component;

    protected CustomComponent()
    {
        _component = new Lazy<IComponent>(BuildBody);
    }

    public Size Size => _component.Value.Size;

    public void Accept(IComponentVisitor visitor)
        => _component.Value.Accept(visitor);

    public IComponent WithStyleApplied(Style style)
        => this.WithStyleAppliedInternal(style);

    public IComponent WrappedInto(Func<IComponent, IComponent> wrapper)
        => this.WrappedIntoInternal(wrapper);

    protected abstract IComponent BuildBody();
}