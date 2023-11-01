using System.Collections;
using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets;

public abstract class CustomRowComponent : IRowComponent
{
    private readonly Lazy<IRowComponent> _component;

    protected CustomRowComponent()
    {
        _component = new Lazy<IRowComponent>(BuildBody);
    }

    public IRowComponent WithStyleApplied(Style style)
        => _component.Value.WithStyleApplied(style);

    public IRowComponent WrappedInto(Func<IComponent, IComponent> wrapper)
        => _component.Value.WrappedInto(wrapper);

    public IEnumerator<IBaseComponent> GetEnumerator()
        => _component.Value.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    protected abstract IRowComponent BuildBody();
}