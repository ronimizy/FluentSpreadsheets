using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets;

public static class ComponentAlignmentExtensions
{
    public static IComponentSource WithContentAlignment(
        this IComponentSource componentSource,
        HorizontalAlignment horizontalAlignment,
        VerticalAlignment verticalAlignment)
    {
        var alignment = new Alignment(verticalAlignment, horizontalAlignment);
        var style = alignment.AsStyle();

        return componentSource.WithStyle(style);
    }
    
    public static IComponentSource WithContentAlignment(
        this IComponentSource componentSource,
        HorizontalAlignment horizontalAlignment)
    {
        var alignment = new Alignment { Horizontal = horizontalAlignment };
        var style = alignment.AsStyle();

        return componentSource.WithStyle(style);
    }
    
    public static IComponentSource WithContentAlignment(
        this IComponentSource componentSource,
        VerticalAlignment verticalAlignment)
    {
        var alignment = new Alignment { Vertical = verticalAlignment };
        var style = alignment.AsStyle();

        return componentSource.WithStyle(style);
    }
}