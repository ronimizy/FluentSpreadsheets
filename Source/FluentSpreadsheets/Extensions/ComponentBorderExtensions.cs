using System.Drawing;
using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets;

public static class ComponentBorderExtensions
{
    public static IComponent WithLeadingBorder(this IComponent component, BorderStyle borderStyle)
    {
        var frameStyle = new FrameBorderStyle { Leading = borderStyle };
        var style = frameStyle.AsStyle();

        return component.WrapInStyle(style);
    }

    public static IComponent WithLeadingBorder(this IComponent component, BorderType? borderType, Color? color)
        => component.WithLeadingBorder(new BorderStyle(borderType, color));

    public static IComponent WithLeadingBorderType(this IComponent component, BorderType borderType)
        => component.WithLeadingBorder(borderType, null);

    public static IComponent WithLeadingBorderColor(this IComponent component, Color color)
        => component.WithLeadingBorder(null, color);

    public static IComponent WithTopBorder(this IComponent component, BorderStyle borderStyle)
    {
        var frameStyle = new FrameBorderStyle { Top = borderStyle };
        var style = frameStyle.AsStyle();

        return component.WrapInStyle(style);
    }

    public static IComponent WithTopBorder(this IComponent component, BorderType? borderType, Color? color)
        => component.WithTopBorder(new BorderStyle(borderType, color));

    public static IComponent WithTopBorderType(this IComponent component, BorderType borderType)
        => component.WithTopBorder(borderType, null);

    public static IComponent WithTopBorderColor(this IComponent component, Color color)
        => component.WithTopBorder(null, color);

    public static IComponent WithBottomBorder(this IComponent component, BorderStyle borderStyle)
    {
        var frameStyle = new FrameBorderStyle { Bottom = borderStyle };
        var style = frameStyle.AsStyle();

        return component.WrapInStyle(style);
    }

    public static IComponent WithBottomBorder(this IComponent component, BorderType? borderType, Color? color)
        => component.WithBottomBorder(new BorderStyle(borderType, color));

    public static IComponent WithBottomBorderType(this IComponent component, BorderType borderType)
        => component.WithBottomBorder(borderType, null);

    public static IComponent WithBottomBorderColor(this IComponent component, Color color)
        => component.WithBottomBorder(null, color);

    public static IComponent WithTrailingBorder(this IComponent component, BorderStyle borderStyle)
    {
        var frameStyle = new FrameBorderStyle { Trailing = borderStyle };
        var style = frameStyle.AsStyle();

        return component.WrapInStyle(style);
    }

    public static IComponent WithTrailingBorder(this IComponent component, BorderType? borderType, Color? color)
        => component.WithTrailingBorder(new BorderStyle(borderType, color));

    public static IComponent WithTrailingBorderType(this IComponent component, BorderType borderType)
        => component.WithTrailingBorder(borderType, null);

    public static IComponent WithTrailingBorderColor(this IComponent component, Color color)
        => component.WithTrailingBorder(null, color);
}