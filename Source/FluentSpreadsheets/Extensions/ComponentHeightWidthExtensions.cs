using FluentSpreadsheets.ComponentImplementations;

namespace FluentSpreadsheets;

public static class ComponentHeightWidthExtensions
{
    public static IComponentSource WithRowHeight(this IComponentSource component, int height)
        => component.WrappedInto(x => new RowHeightComponent(x, height));

    public static IComponentSource WithColumnWidth(this IComponentSource component, int width)
        => component.WrappedInto(x => new ColumnWidthComponent(x, width));
}