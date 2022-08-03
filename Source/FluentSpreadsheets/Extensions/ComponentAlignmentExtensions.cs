using FluentSpreadsheets.ComponentImplementations.StylingComponents.Alignment;

namespace FluentSpreadsheets;

public static class ComponentAlignmentExtensions
{
    public static IComponent WithContentAlignment(
        this IComponent component,
        HorizontalAlignment horizontalAlignment,
        VerticalAlignment verticalAlignment)
    {
        return new AlignmentComponent(component, horizontalAlignment, verticalAlignment);
    }

    public static IComponent WithContentAlignment(
        this IComponent component,
        HorizontalAlignment horizontalAlignment)
    {
        return new AlignmentComponent(component, horizontalAlignment, VerticalAlignment.Unspecified);
    }

    public static IComponent WithContentAlignment(
        this IComponent component,
        VerticalAlignment verticalAlignment)
    {
        return new AlignmentComponent(component, HorizontalAlignment.Unspecified, verticalAlignment);
    }
}