using System.Collections;

namespace FluentSpreadsheets;

public class CustomizerRowComponent : ICustomizerRowComponent
{
    private readonly IRowComponent _row;
    private readonly Func<IComponent, IComponentSource> _customizer;

    public CustomizerRowComponent(IRowComponent row, Func<IComponent, IComponentSource> customizer)
    {
        _row = row;
        _customizer = customizer;
    }

    public IEnumerator<IComponentSource> GetEnumerator()
        => _row.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public IComponentSource Customize(IComponent component)
        => _customizer.Invoke(component);
}