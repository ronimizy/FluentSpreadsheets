using FluentSpreadsheets.ComponentSourceImplementations;

namespace FluentSpreadsheets;

public static class ComponentSourceExtensions
{
    public static IComponentSource CustomizedWith(
        this IComponentSource componentSource,
        Func<IComponent, IComponent> customizer)
    {
        return new CustomizerComponentSource(componentSource, customizer);
    }
}