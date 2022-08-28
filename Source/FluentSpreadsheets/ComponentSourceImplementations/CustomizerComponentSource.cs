using System.Collections;

namespace FluentSpreadsheets.ComponentSourceImplementations;

internal class CustomizerComponentSource : ICustomizerComponentSource
{
    private readonly IComponentSource _componentSource;
    private readonly Func<IComponentSource, IComponentSource> _customizer;

    public CustomizerComponentSource(
        IComponentSource componentSource,
        Func<IComponentSource, IComponentSource> customizer)
    {
        _componentSource = componentSource;
        _customizer = customizer;
    }

    public IEnumerator<IComponentSource> GetEnumerator()
        => _componentSource.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public IComponentSource Customize(IComponentSource componentSource)
        => _customizer.Invoke(componentSource);
}