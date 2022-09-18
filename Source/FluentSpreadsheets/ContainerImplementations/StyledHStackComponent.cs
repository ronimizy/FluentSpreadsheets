using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.ContainerImplementations;

internal sealed class StyledHStackComponent : HStackComponentBase
{
    private readonly IHStackComponent _component;
    
    public StyledHStackComponent(IHStackComponent component, Style style) : base(component.Style.Apply(style))
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

    public override IHStackComponent WrappedInto(Func<IComponent, IComponent> wrapper)
    {
        var modified = new ModifierHStackComponent(_component, wrapper);
        return new StyledHStackComponent(modified, Style);
    }

    public override IHStackComponent WithStyleApplied(Style style)
        => new StyledHStackComponent(_component, Style.Apply(style));
}