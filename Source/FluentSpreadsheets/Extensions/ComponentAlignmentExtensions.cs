using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public static class ComponentAlignmentExtensions
{
    public static T WithContentAlignment<T>(
        this T wrappable,
        HorizontalAlignment horizontalAlignment,
        VerticalAlignment verticalAlignment)
        where T : IWrappable<T>
    {
        var alignment = new Alignment(verticalAlignment, horizontalAlignment);
        var style = alignment.AsStyle();

        return wrappable.WithStyleApplied(style);
    }

    public static T WithContentAlignment<T>(
        this T wrappable,
        HorizontalAlignment horizontalAlignment)
        where T : IWrappable<T>
    {
        var alignment = new Alignment { Horizontal = horizontalAlignment };
        var style = alignment.AsStyle();

        return wrappable.WithStyleApplied(style);
    }

    public static T WithContentAlignment<T>(
        this T wrappable,
        VerticalAlignment verticalAlignment)
        where T : IWrappable<T>
    {
        var alignment = new Alignment { Vertical = verticalAlignment };
        var style = alignment.AsStyle();

        return wrappable.WithStyleApplied(style);
    }
}