using FluentSpreadsheets.ComponentImplementations;

namespace FluentSpreadsheets;

public static class ComponentScaleExtensions
{
    public static IComponent ScaledBy(this IComponent component, int factor, Axis axis)
    {
        var verticalFactor = 1;
        var horizontalFactor = 1;

        if (axis.HasFlag(Axis.Vertical))
            verticalFactor *= factor;
        
        if (axis.HasFlag(Axis.Horizontal))
            horizontalFactor *= factor;

        var scale = new Scale(horizontalFactor, verticalFactor);
        return new ScaledComponent(component, scale);
    }
}