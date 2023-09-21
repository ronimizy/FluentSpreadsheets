using FluentSpreadsheets.Implementations;

namespace FluentSpreadsheets;

public static class ComponentGroupExtensions
{
    public static IComponentGroup CustomizedWith(
        this IComponentGroup componentGroup,
        Func<IComponent, IComponent> customizer)
    {
        return new CustomizerComponentGroup(componentGroup, customizer);
    }
}