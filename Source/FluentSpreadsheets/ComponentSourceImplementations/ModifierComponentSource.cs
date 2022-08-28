using System.Collections;

namespace FluentSpreadsheets.ComponentSourceImplementations;

internal class ModifierComponentSource : IComponentSource
{
    private readonly IComponentSource _source;
    private readonly Func<IComponent, IComponent> _modifier;

    public ModifierComponentSource(IComponentSource source, Func<IComponent, IComponent> modifier)
    {
        _source = source;
        _modifier = modifier;
    }

    public IEnumerator<IComponentSource> GetEnumerator()
        => _source.ExtractComponents().Select(_modifier).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}