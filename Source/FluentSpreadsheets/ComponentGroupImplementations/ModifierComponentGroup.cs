namespace FluentSpreadsheets.ComponentGroupImplementations;

internal class ModifierComponentGroup : ComponentGroupBase
{
    private readonly IComponentGroup _group;
    private readonly Func<IComponent, IComponent> _modifier;

    public ModifierComponentGroup(IComponentGroup group, Func<IComponent, IComponent> modifier)
    {
        _group = group;
        _modifier = modifier;
    }

    public override IEnumerator<IBaseComponent> GetEnumerator()
        => _group.ExtractComponents().Select(_modifier).GetEnumerator();
}