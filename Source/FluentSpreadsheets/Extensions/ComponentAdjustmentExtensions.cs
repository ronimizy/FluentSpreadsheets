using FluentSpreadsheets.ComponentImplementations;

namespace FluentSpreadsheets;

public static class ComponentAdjustmentExtensions
{
    public static IComponent WithAdjustedRows(this IComponent component)
        => new AdjustedComponent(component, true, false);

    public static IComponent WithAdjustedColumns(this IComponent component)
        => new AdjustedComponent(component, false, true);
    
    public static IComponent Adjusted(this IComponent component)
        => new AdjustedComponent(component, true, true);
}