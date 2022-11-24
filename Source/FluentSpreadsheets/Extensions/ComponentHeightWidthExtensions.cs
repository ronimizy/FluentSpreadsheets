using FluentSpreadsheets.ComponentImplementations;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public static class ComponentHeightWidthExtensions
{
    public static T WithRowHeight<T>(this T component, RelativeSize height) where T : IWrappable<T>
        => component.WrappedInto(x => new RowHeightComponent(x, height));

    public static T WithColumnWidth<T>(this T component, RelativeSize width) where T : IWrappable<T>
        => component.WrappedInto(x => new ColumnWidthComponent(x, width));
}