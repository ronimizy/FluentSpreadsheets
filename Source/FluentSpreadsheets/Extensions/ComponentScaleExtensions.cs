using FluentSpreadsheets.ComponentImplementations;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public static class ComponentScaleExtensions
{
    public static T ScaledBy<T>(this T component, int factor, Axis axis) where T : IWrappable<T>
    {
        var verticalFactor = 1;
        var horizontalFactor = 1;

        if (axis.HasFlag(Axis.Vertical))
            verticalFactor *= factor;

        if (axis.HasFlag(Axis.Horizontal))
            horizontalFactor *= factor;

        var scale = new Scale(horizontalFactor, verticalFactor);
        return component.WrappedInto(x => new ScaledComponent(x, scale));
    }
}