using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets;

internal static class BaseComponentExtensions
{
    internal static IEnumerable<IComponent> ExtractComponents(this IEnumerable<IBaseComponent> enumerable)
    {
        foreach (var baseComponent in enumerable)
        {
            if (baseComponent is IComponent component)
            {
                yield return component;
            }
            else if (baseComponent is IComponentSource source)
            {
                foreach (var innerComponent in source.ExtractComponents())
                {
                    yield return innerComponent;
                }
            }
            else
            {
                throw new InvalidOperationException("Unknown component type");
            }
        }
    }

    internal static IBaseComponent WithStyleApplied(
        this IBaseComponent baseComponent,
        Style style)
    {
        return baseComponent switch
        {
            IComponent component => component.WithStyleApplied(style),
            IComponentSource source => source.WithStyleApplied(style),
            _ => throw new InvalidOperationException("Unknown component type")
        };
    }

    internal static IBaseComponent WrappedInto(
        this IBaseComponent baseComponent,
        Func<IComponent, IComponent> wrapper)
    {
        return baseComponent switch
        {
            IComponent component => wrapper(component),
            IComponentSource source => source.WrappedInto(wrapper),
            _ => throw new InvalidOperationException("Unknown component type")
        };
    }
}