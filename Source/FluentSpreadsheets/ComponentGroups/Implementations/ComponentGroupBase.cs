using System.Collections;
using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets.Implementations;

internal abstract class ComponentGroupBase : IComponentGroup
{
    protected ComponentGroupBase(Style style = default)
    {
        Style = style;
    }

    public Style Style { get; }

    public abstract IEnumerator<IBaseComponent> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public virtual IComponentGroup WithStyleApplied(Style style)
        => new StylingComponentGroup(this, style);

    public virtual IComponentGroup WrappedInto(Func<IComponent, IComponent> wrapper)
        => new ModifierComponentGroup(this, wrapper);

    IBaseComponent IWrappable<IBaseComponent>.WrappedInto(Func<IComponent, IComponent> wrapper)
        => WrappedInto(wrapper);

    IBaseComponent IWrappable<IBaseComponent>.WithStyleApplied(Style style)
        => WithStyleApplied(style);
}