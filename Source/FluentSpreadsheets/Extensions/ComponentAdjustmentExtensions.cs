using FluentSpreadsheets.ComponentImplementations;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public static class ComponentAdjustmentExtensions
{
    public static T WithAdjustedRows<T>(this T component) where T : IWrappable<T>
        => component.WrappedInto(x => new AdjustedComponent(x, true, false));

    public static T WithAdjustedColumns<T>(this T component) where T : IWrappable<T>
        => component.WrappedInto(x => new AdjustedComponent(x, false, true));

    public static T Adjusted<T>(this T component) where T : IWrappable<T>
        => component.WrappedInto(x => new AdjustedComponent(x, true, true));
}