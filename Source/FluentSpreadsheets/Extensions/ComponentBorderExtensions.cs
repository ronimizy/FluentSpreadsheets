using System.Drawing;
using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public static class ComponentBorderExtensions
{
    public static T WithLeadingBorder<T>(this T component, BorderStyle borderStyle) where T : IWrappable<T>
    {
        var frameStyle = new FrameBorderStyle { Leading = borderStyle };
        var style = frameStyle.AsStyle();

        return component.WithStyleApplied(style);
    }

    public static T WithLeadingBorder<T>(this T component, BorderType? borderType, Color? color) where T : IWrappable<T>
        => component.WithLeadingBorder(new BorderStyle(borderType, color));

    public static T WithLeadingBorderType<T>(this T component, BorderType borderType) where T : IWrappable<T>
        => component.WithLeadingBorder(borderType, null);

    public static T WithLeadingBorderColor<T>(this T component, Color color) where T : IWrappable<T>
        => component.WithLeadingBorder(null, color);

    public static T WithTrailingBorder<T>(this T component, BorderStyle borderStyle) where T : IWrappable<T>
    {
        var frameStyle = new FrameBorderStyle { Trailing = borderStyle };
        var style = frameStyle.AsStyle();

        return component.WithStyleApplied(style);
    }

    public static T WithTrailingBorder<T>(this T component, BorderType? borderType, Color? color) where T : IWrappable<T>
        => component.WithTrailingBorder(new BorderStyle(borderType, color));

    public static T WithTrailingBorderType<T>(this T component, BorderType borderType) where T : IWrappable<T>
        => component.WithTrailingBorder(borderType, null);

    public static T WithTrailingBorderColor<T>(this T component, Color color) where T : IWrappable<T>
        => component.WithTrailingBorder(null, color);

    public static T WithTopBorder<T>(this T component, BorderStyle borderStyle) where T : IWrappable<T>
    {
        var frameStyle = new FrameBorderStyle { Top = borderStyle };
        var style = frameStyle.AsStyle();

        return component.WithStyleApplied(style);
    }

    public static T WithTopBorder<T>(this T component, BorderType? borderType, Color? color) where T : IWrappable<T>
        => component.WithTopBorder(new BorderStyle(borderType, color));

    public static T WithTopBorderType<T>(this T component, BorderType borderType) where T : IWrappable<T>
        => component.WithTopBorder(borderType, null);

    public static T WithTopBorderColor<T>(this T component, Color color) where T : IWrappable<T>
        => component.WithTopBorder(null, color);

    public static T WithBottomBorder<T>(this T component, BorderStyle borderStyle) where T : IWrappable<T>
    {
        var frameStyle = new FrameBorderStyle { Bottom = borderStyle };
        var style = frameStyle.AsStyle();

        return component.WithStyleApplied(style);
    }

    public static T WithBottomBorder<T>(this T component, BorderType? borderType, Color? color) where T : IWrappable<T>
        => component.WithBottomBorder(new BorderStyle(borderType, color));

    public static T WithBottomBorderType<T>(this T component, BorderType borderType) where T : IWrappable<T>
        => component.WithBottomBorder(borderType, null);

    public static T WithBottomBorderColor<T>(this T component, Color color) where T : IWrappable<T>
        => component.WithBottomBorder(null, color);
}