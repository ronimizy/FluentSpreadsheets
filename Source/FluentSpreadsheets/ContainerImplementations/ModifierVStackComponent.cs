namespace FluentSpreadsheets.ContainerImplementations;

internal sealed class ModifierVStackComponent : VStackComponentBase
{
    private readonly IVStackComponent _component;
    private readonly Func<IComponent, IComponent> _modifier;

    public ModifierVStackComponent(IVStackComponent component, Func<IComponent, IComponent> modifier) 
        : base(component.Style)
    {
        _component = component;
        _modifier = modifier;
        Size = component.Size;
    }

    public override IEnumerator<IComponent> GetEnumerator()
    {
        return _component
            .Select(x => x.WrappedInto(_modifier))
            .GetEnumerator();
    }
}