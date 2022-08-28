using FluentSpreadsheets.ComponentImplementations;

namespace FluentSpreadsheets;

public static class ComponentAdjustmentExtensions
{
    public static IComponentSource WithAdjustedRows(this IComponentSource component)
        => component.WrappedInto(x => new AdjustedComponent(x, true, false));

    public static IComponentSource WithAdjustedColumns(this IComponentSource component)
        => component.WrappedInto(x => new AdjustedComponent(x, false, true));

    public static IComponentSource Adjusted(this IComponentSource component)
        => component.WrappedInto(x => new AdjustedComponent(x, true, true));
}