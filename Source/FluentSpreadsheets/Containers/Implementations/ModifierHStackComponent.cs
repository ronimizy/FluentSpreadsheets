namespace FluentSpreadsheets.Implementations;

internal sealed class ModifierHStackComponent : HStackComponentBase
{
    private readonly IHStackComponent _component;
    private readonly Func<IComponent, IComponent> _modifier;

    public ModifierHStackComponent(IHStackComponent component, Func<IComponent, IComponent> modifier)
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