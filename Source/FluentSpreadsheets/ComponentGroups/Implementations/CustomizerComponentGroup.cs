using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.Implementations;

internal sealed class CustomizerComponentGroup : ComponentGroupBase, ICustomizerComponentGroup
{
    private readonly IComponentGroup _componentGroup;
    private readonly Func<IComponent, IComponent> _customizer;

    public CustomizerComponentGroup(
        IComponentGroup componentGroup,
        Func<IComponent, IComponent> customizer)
        : base(componentGroup.Style)
    {
        _componentGroup = componentGroup;
        _customizer = customizer;
    }

    public IComponent Customize(IComponent component)
        => _customizer.Invoke(component);

    public override IEnumerator<IBaseComponent> GetEnumerator()
        => _componentGroup.GetEnumerator();

    public override IComponentGroup WithStyleApplied(Style style)
    {
        IComponent Customizer(IComponent x)
        {
            var modified = _customizer.Invoke(x);
            return modified.WithStyleApplied(style.Apply(modified.Style));
        }

        return new CustomizerComponentGroup(_componentGroup, Customizer);
    }

    public override IComponentGroup WrappedInto(Func<IComponent, IComponent> wrapper)
    {
        IComponent Customizer(IComponent x)
            => _customizer.Invoke(x).WrappedInto(wrapper);
        
        return new CustomizerComponentGroup(_componentGroup, Customizer);
    }
}