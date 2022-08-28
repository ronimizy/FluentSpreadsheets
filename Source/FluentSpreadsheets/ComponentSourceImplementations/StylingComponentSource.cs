using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.ComponentSourceImplementations;

internal class StylingComponentSource : ComponentSourceBase, IStylingComponentSource
{
    private readonly IComponentSource _componentSource;
    private readonly Style _style;

    public StylingComponentSource(IComponentSource componentSource, Style style)
    {
        _componentSource = componentSource;
        _style = style;
    }

    public override IEnumerator<IBaseComponent> GetEnumerator()
    {
        return _componentSource.ExtractComponents().Select(x => x.WithStyleApplied(_style)).GetEnumerator();
    }

    public override IComponentSource WithStyleApplied(Style style)
    {
        var newStyle = _style.Apply(style);
        return new StylingComponentSource(_componentSource, newStyle);
    }

    public override IComponentSource WrappedInto(Func<IComponent, IComponent> wrapper)
    {
        var wrapped = new ModifierComponentSource(_componentSource, wrapper);
        return new StylingComponentSource(wrapped, _style);
    }
}