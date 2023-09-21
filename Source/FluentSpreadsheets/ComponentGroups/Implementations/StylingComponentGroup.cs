using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.Implementations;

internal sealed class StylingComponentGroup : ComponentGroupBase, IStylingComponentGroup
{
    private readonly IComponentGroup _componentGroup;

    public StylingComponentGroup(IComponentGroup componentGroup, Style style) : base(style)
    {
        _componentGroup = componentGroup;
    }

    public override IEnumerator<IBaseComponent> GetEnumerator()
    {
        return _componentGroup
            .ExtractComponents()
            .Select(x => x.WithStyleApplied(Style.Apply(x.Style)))
            .GetEnumerator();
    }

    public override IComponentGroup WithStyleApplied(Style style)
    {
        var newStyle = Style.Apply(style);
        return new StylingComponentGroup(_componentGroup, newStyle);
    }

    public override IComponentGroup WrappedInto(Func<IComponent, IComponent> wrapper)
    {
        var wrapped = new ModifierComponentGroup(_componentGroup, wrapper);
        return new StylingComponentGroup(wrapped, Style);
    }
}