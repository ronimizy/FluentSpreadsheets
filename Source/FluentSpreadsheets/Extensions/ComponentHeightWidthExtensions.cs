using FluentSpreadsheets.ComponentImplementations;

namespace FluentSpreadsheets;

public static class ComponentHeightWidthExtensions
{
    public static IComponent WithRowHeight(this IComponent component, int height)
        => new RowHeightComponent(component, height);

    public static IComponent WithColumnWidth(this IComponent component, int width)
        => new ColumnWidthComponent(component, width);
}