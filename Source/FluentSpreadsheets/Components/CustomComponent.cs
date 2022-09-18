using FluentSpreadsheets.ComponentImplementations;
using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public abstract class CustomComponent : IComponent
{
    private readonly Lazy<IComponent> _component;

    protected CustomComponent()
    {
        _component = new Lazy<IComponent>(BuildBody);
    }

    public Size Size => _component.Value.Size;
    public Style Style => _component.Value.Style;

    public void Accept(IComponentVisitor visitor)
        => _component.Value.Accept(visitor);

    public IComponent WithStyleApplied(Style style)
        => new StylingComponent(this, style);

    public IComponent WrappedInto(Func<IComponent, IComponent> wrapper)
        => wrapper.Invoke(this);

    IBaseComponent IWrappable<IBaseComponent>.WrappedInto(Func<IComponent, IComponent> wrapper)
        => WrappedInto(wrapper);

    IBaseComponent IWrappable<IBaseComponent>.WithStyleApplied(Style style)
        => WithStyleApplied(style);

    protected abstract IComponent BuildBody();
}