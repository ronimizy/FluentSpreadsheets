using System.Drawing;
using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets;

public static class ComponentBorderExtensions
{
    public static IComponentSource WithLeadingBorder(this IComponentSource component, BorderStyle borderStyle)
    {
        var frameStyle = new FrameBorderStyle { Leading = borderStyle };
        var style = frameStyle.AsStyle();

        return component.WithStyle(style);
    }

    public static IComponentSource WithLeadingBorder(this IComponentSource component, BorderType? borderType, Color? color)
        => component.WithLeadingBorder(new BorderStyle(borderType, color));

    public static IComponentSource WithLeadingBorderType(this IComponentSource component, BorderType borderType)
        => component.WithLeadingBorder(borderType, null);

    public static IComponentSource WithLeadingBorderColor(this IComponentSource component, Color color)
        => component.WithLeadingBorder(null, color);

    public static IComponentSource WithTopBorder(this IComponentSource component, BorderStyle borderStyle)
    {
        var frameStyle = new FrameBorderStyle { Top = borderStyle };
        var style = frameStyle.AsStyle();

        return component.WithStyle(style);
    }

    public static IComponentSource WithTopBorder(this IComponentSource component, BorderType? borderType, Color? color)
        => component.WithTopBorder(new BorderStyle(borderType, color));

    public static IComponentSource WithTopBorderType(this IComponentSource component, BorderType borderType)
        => component.WithTopBorder(borderType, null);

    public static IComponentSource WithTopBorderColor(this IComponentSource component, Color color)
        => component.WithTopBorder(null, color);

    public static IComponentSource WithBottomBorder(this IComponentSource component, BorderStyle borderStyle)
    {
        var frameStyle = new FrameBorderStyle { Bottom = borderStyle };
        var style = frameStyle.AsStyle();

        return component.WithStyle(style);
    }

    public static IComponentSource WithBottomBorder(this IComponentSource component, BorderType? borderType, Color? color)
        => component.WithBottomBorder(new BorderStyle(borderType, color));

    public static IComponentSource WithBottomBorderType(this IComponentSource component, BorderType borderType)
        => component.WithBottomBorder(borderType, null);

    public static IComponentSource WithBottomBorderColor(this IComponentSource component, Color color)
        => component.WithBottomBorder(null, color);

    public static IComponentSource WithTrailingBorder(this IComponentSource component, BorderStyle borderStyle)
    {
        var frameStyle = new FrameBorderStyle { Trailing = borderStyle };
        var style = frameStyle.AsStyle();

        return component.WithStyle(style);
    }

    public static IComponentSource WithTrailingBorder(this IComponentSource component, BorderType? borderType, Color? color)
        => component.WithTrailingBorder(new BorderStyle(borderType, color));

    public static IComponentSource WithTrailingBorderType(this IComponentSource component, BorderType borderType)
        => component.WithTrailingBorder(borderType, null);

    public static IComponentSource WithTrailingBorderColor(this IComponentSource component, Color color)
        => component.WithTrailingBorder(null, color);
}