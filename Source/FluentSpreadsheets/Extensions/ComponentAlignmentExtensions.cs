using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets;

public static class ComponentAlignmentExtensions
{
    public static IComponent WithContentAlignment(
        this IComponent component,
        HorizontalAlignment horizontalAlignment,
        VerticalAlignment verticalAlignment)
    {
        var alignment = new Alignment(verticalAlignment, horizontalAlignment);
        var style = alignment.AsStyle();

        return component.WrapInStyle(style);
    }

    public static IComponent WithContentAlignment(
        this IComponent component,
        HorizontalAlignment horizontalAlignment)
    {
        var alignment = new Alignment { Horizontal = horizontalAlignment };
        var style = alignment.AsStyle();

        return component.WrapInStyle(style);
    }

    public static IComponent WithContentAlignment(
        this IComponent component,
        VerticalAlignment verticalAlignment)
    {
        var alignment = new Alignment { Vertical = verticalAlignment };
        var style = alignment.AsStyle();

        return component.WrapInStyle(style);
    }
}