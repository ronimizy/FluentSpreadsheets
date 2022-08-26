using FluentSpreadsheets.ComponentImplementations;

namespace FluentSpreadsheets;

public static class ComponentHeightWidthExtensions
{
    public static IComponent WithRowHeight(this IComponent component, int height)
        => component.Wrap(x => new RowHeightComponent(x, height));

    public static IComponent WithColumnWidth(this IComponent component, int width)
        => component.Wrap(x => new ColumnWidthComponent(x, width));
}