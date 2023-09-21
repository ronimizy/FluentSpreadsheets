using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.Implementations;

internal sealed class StylingRowComponent : RowComponentBase
{
    private readonly IRowComponent _component;
    private readonly Style _style;

    public StylingRowComponent(IRowComponent component, Style style)
    {
        _component = component;
        _style = style;
    }

    public override IEnumerator<IBaseComponent> GetEnumerator()
    {
        return _component
            .Select(x => x.WithStyleApplied(_style.Apply(x.Style)))
            .GetEnumerator();
    }

    public override IRowComponent WithStyleApplied(Style style)
        => new StylingRowComponent(_component, _style.Apply(style));
}