using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.ComponentGroupImplementations;

internal class StylingComponentGroup : ComponentGroupBase, IStylingComponentGroup
{
    private readonly IComponentGroup _componentGroup;
    private readonly Style _style;

    public StylingComponentGroup(IComponentGroup componentGroup, Style style)
    {
        _componentGroup = componentGroup;
        _style = style;
    }

    public override IEnumerator<IBaseComponent> GetEnumerator()
    {
        return _componentGroup
            .ExtractComponents()
            .Select(x => x.WithStyleApplied(_style))
            .GetEnumerator();
    }

    public override IComponentGroup WithStyleApplied(Style style)
    {
        var newStyle = _style.Apply(style);
        return new StylingComponentGroup(_componentGroup, newStyle);
    }

    public override IComponentGroup WrappedInto(Func<IComponent, IComponent> wrapper)
    {
        var wrapped = new ModifierComponentGroup(_componentGroup, wrapper);
        return new StylingComponentGroup(wrapped, _style);
    }
}