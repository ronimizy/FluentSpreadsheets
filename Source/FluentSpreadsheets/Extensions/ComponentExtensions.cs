using FluentSpreadsheets.ComponentImplementations;
using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets;

internal static class ComponentExtensions
{
    public static IComponent Wrap(this IComponent component, Func<IComponent, IComponent> wrapper)
    {
        if (component is not IStylingComponent stylingComponent)
            return wrapper.Invoke(component);

        var wrappedComponent = wrapper.Invoke(stylingComponent.StyledComponent);
        return new StylingComponent(wrappedComponent, stylingComponent.Style);
    }

    public static IComponent WrapInStyle(this IComponent component, Style style)
    {
        if (component is not IStylingComponent stylingComponent)
            return new StylingComponent(component, style);

        var newStyle = stylingComponent.Style.Apply(style);
        return new StylingComponent(stylingComponent.StyledComponent, newStyle);
    }
}