using System.Collections;
using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.ComponentGroupImplementations;

internal abstract class ComponentGroupBase : IComponentGroup
{
    public abstract IEnumerator<IBaseComponent> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public virtual IComponentGroup WithStyleApplied(Style style)
        => new StylingComponentGroup(this, style);

    public virtual IComponentGroup WrappedInto(Func<IComponent, IComponent> wrapper)
        => new ModifierComponentGroup(this, wrapper);
}