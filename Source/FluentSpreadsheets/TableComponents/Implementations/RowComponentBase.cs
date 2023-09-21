using System.Collections;
using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.Implementations;

internal abstract class RowComponentBase : IRowComponent
{
    public virtual IRowComponent WithStyleApplied(Style style)
        => new StylingRowComponent(this, style);

    public virtual IRowComponent WrappedInto(Func<IComponent, IComponent> wrapper)
    {
        IEnumerable<IBaseComponent> enumerable = this.Select(x => x.WrappedInto(wrapper));
        return new RowComponent(enumerable);
    }

    public abstract IEnumerator<IBaseComponent> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}