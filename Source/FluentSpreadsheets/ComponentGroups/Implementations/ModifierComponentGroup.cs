namespace FluentSpreadsheets.Implementations;

internal sealed class ModifierComponentGroup : ComponentGroupBase
{
    private readonly IComponentGroup _group;
    private readonly Func<IComponent, IComponent> _modifier;

    public ModifierComponentGroup(IComponentGroup group, Func<IComponent, IComponent> modifier) : base(group.Style)
    {
        _group = group;
        _modifier = modifier;
    }

    public override IEnumerator<IBaseComponent> GetEnumerator()
        => _group.ExtractComponents().Select(x => x.WrappedInto(_modifier)).GetEnumerator();
}