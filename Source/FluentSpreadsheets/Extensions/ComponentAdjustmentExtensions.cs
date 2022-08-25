using FluentSpreadsheets.ComponentImplementations;

namespace FluentSpreadsheets;

public static class ComponentAdjustmentExtensions
{
    public static IComponent WithAdjustedRows(this IComponent component)
        => component.Wrap(x => new AdjustedComponent(x, true, false));

    public static IComponent WithAdjustedColumns(this IComponent component)
        => component.Wrap(x => new AdjustedComponent(x, false, true));

    public static IComponent Adjusted(this IComponent component)
        => component.Wrap(x => new AdjustedComponent(component, true, true));
}