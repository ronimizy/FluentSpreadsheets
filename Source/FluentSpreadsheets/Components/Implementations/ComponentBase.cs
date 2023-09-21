using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets.Implementations;

internal abstract class ComponentBase : IComponent
{
    protected ComponentBase(Style style = default)
    {
        Style = style;
    }

    public abstract Size Size { get; }

    public Style Style { get; }

    public abstract void Accept(IComponentVisitor visitor);

    public virtual IComponent WithStyleApplied(Style style)
        => new StylingComponent(this, style);

    public virtual IComponent WrappedInto(Func<IComponent, IComponent> wrapper)
        => wrapper.Invoke(this);

    IBaseComponent IWrappable<IBaseComponent>.WrappedInto(Func<IComponent, IComponent> wrapper)
        => WrappedInto(wrapper);

    IBaseComponent IWrappable<IBaseComponent>.WithStyleApplied(Style style)
        => WithStyleApplied(style);
}