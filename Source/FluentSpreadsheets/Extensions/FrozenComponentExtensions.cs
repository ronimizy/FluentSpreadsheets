using FluentSpreadsheets.Implementations;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public static class FrozenComponentExtensions
{
    public static T WithFrozenRows<T>(this T component) where T : IWrappable<T>
        => component.WrappedInto(x => new FrozenComponent(x, true, false));

    public static T WithFrozenColumns<T>(this T component) where T : IWrappable<T>
        => component.WrappedInto(x => new FrozenComponent(x, false, true));
    
    public static T Frozen<T>(this T component) where T : IWrappable<T>
        => component.WrappedInto(x => new FrozenComponent(x, true, true));
}