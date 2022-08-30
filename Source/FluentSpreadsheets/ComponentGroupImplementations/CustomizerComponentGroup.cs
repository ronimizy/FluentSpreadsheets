namespace FluentSpreadsheets.ComponentGroupImplementations;

internal class CustomizerComponentGroup : ComponentGroupBase, ICustomizerComponentGroup
{
    private readonly IComponentGroup _componentGroup;
    private readonly Func<IComponent, IComponent> _customizer;

    public CustomizerComponentGroup(
        IComponentGroup componentGroup,
        Func<IComponent, IComponent> customizer)
    {
        _componentGroup = componentGroup;
        _customizer = customizer;
    }


    public IComponent Customize(IComponent component)
        => _customizer.Invoke(component);

    public override IEnumerator<IBaseComponent> GetEnumerator()
        => _componentGroup.GetEnumerator();
}