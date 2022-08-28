namespace FluentSpreadsheets.ComponentSourceImplementations;

internal class CustomizerComponentSource : ComponentSourceBase, ICustomizerComponentSource
{
    private readonly IComponentSource _componentSource;
    private readonly Func<IComponent, IComponent> _customizer;

    public CustomizerComponentSource(
        IComponentSource componentSource,
        Func<IComponent, IComponent> customizer)
    {
        _componentSource = componentSource;
        _customizer = customizer;
    }


    public IComponent Customize(IComponent componentSource)
        => _customizer.Invoke(componentSource);

    public override IEnumerator<IBaseComponent> GetEnumerator()
        => _componentSource.GetEnumerator();
}