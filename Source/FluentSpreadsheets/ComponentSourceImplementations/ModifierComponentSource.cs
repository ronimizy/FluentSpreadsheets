namespace FluentSpreadsheets.ComponentSourceImplementations;

internal class ModifierComponentSource : ComponentSourceBase
{
    private readonly IComponentSource _source;
    private readonly Func<IComponent, IComponent> _modifier;

    public ModifierComponentSource(IComponentSource source, Func<IComponent, IComponent> modifier)
    {
        _source = source;
        _modifier = modifier;
    }

    public override IEnumerator<IBaseComponent> GetEnumerator()
        => _source.ExtractComponents().Select(_modifier).GetEnumerator();
}