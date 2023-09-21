using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.Implementations;

internal sealed class StyledVStackComponent : VStackComponentBase
{
    private readonly IVStackComponent _component;
    
    public StyledVStackComponent(IVStackComponent component, Style style) : base(component.Style.Apply(style))
    {
        _component = component;
        Size = component.Size;
    }

    public override IEnumerator<IComponent> GetEnumerator()
    {
        return _component
            .Select(x => x.WithStyleApplied(Style.Apply(x.Style)))
            .GetEnumerator();
    }

    protected override IVStackComponent WrappedIntoProtected(Func<IComponent, IComponent> wrapper)
    {
        var modified = new ModifierVStackComponent(_component, wrapper);
        return new StyledVStackComponent(modified, Style);
    }

    protected override IVStackComponent WithStyleAppliedProtected(Style style)
        => new StyledVStackComponent(_component, Style.Apply(style));
}