using System.Collections;
using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.ComponentSourceImplementations;

internal class StylingComponentSource : IStylingComponentSource
{
    public StylingComponentSource(IComponentSource styledComponentSource, Style style)
    {
        StyledComponentSource = styledComponentSource;
        Style = style;
    }

    public Style Style { get; }
    public IComponentSource StyledComponentSource { get; }

    public IEnumerator<IComponentSource> GetEnumerator()
        => StyledComponentSource.SelectMany(x => x.WithStyle(Style)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}