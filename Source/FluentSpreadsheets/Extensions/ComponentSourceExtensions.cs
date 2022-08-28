using FluentSpreadsheets.ComponentSourceImplementations;
using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets;

public static class ComponentSourceExtensions
{
    public static IComponentSource WithStyle(this IComponentSource componentSource, Style style)
    {
        if (componentSource is IComponent component)
            return component.WithStyle(style);

        if (componentSource is not IStylingComponentSource stylingComponent)
            return new StylingComponentSource(componentSource, style);

        var newStyle = stylingComponent.Style.Apply(style);
        return new StylingComponentSource(stylingComponent.StyledComponentSource, newStyle);
    }

    public static IComponentSource WrappedInto(
        this IComponentSource componentSource,
        Func<IComponent, IComponent> wrapper)
    {
        if (componentSource is IComponent component)
            return component.WrappedInto(wrapper);

        if (componentSource is not IStylingComponent stylingComponent)
            return new ModifierComponentSource(componentSource, wrapper);

        var modifierComponentSource = new ModifierComponentSource(stylingComponent.StyledComponent, wrapper);
        return new StylingComponentSource(modifierComponentSource, stylingComponent.Style);
    }

    public static IComponentSource CustomizedWith(
        this IComponentSource componentSource,
        Func<IComponentSource, IComponentSource> customizer)
    {
        return new CustomizerComponentSource(componentSource, customizer);
    }

    internal static IEnumerable<IComponent> ExtractComponents(this IEnumerable<IComponentSource> enumerable)
    {
        foreach (var source in enumerable)
        {
            if (source is IComponent component)
            {
                yield return component;
            }
            else
            {
                foreach (var innerComponent in source.ExtractComponents())
                {
                    yield return innerComponent;
                }
            }
        }
    }
}