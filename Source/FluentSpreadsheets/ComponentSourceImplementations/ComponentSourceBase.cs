using System.Collections;
using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.ComponentSourceImplementations;

internal abstract class ComponentSourceBase : IComponentSource
{
    public abstract IEnumerator<IBaseComponent> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public virtual IComponentSource WithStyleApplied(Style style)
        => new StylingComponentSource(this, style);

    public virtual IComponentSource WrappedInto(Func<IComponent, IComponent> wrapper)
        => new ModifierComponentSource(this, wrapper);
}