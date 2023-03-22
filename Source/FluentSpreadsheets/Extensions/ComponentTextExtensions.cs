using System.Drawing;
using FluentSpreadsheets.Styles.Text;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public static class ComponentTextExtensions
{
    public static T WithTextStyle<T>(this T component, TextStyle textStyle) where T : IWrappable<T>
    {
        var style = textStyle.AsStyle();
        return component.WithStyleApplied(style);
    }

    public static T WithTextStyle<T>(
        this T component,
        Color? textColor = null,
        TextKind? textKind = null,
        TextWrapping? wrapping = null)
        where T : IWrappable<T>
    {
        return component.WithTextStyle(new TextStyle(textColor, textKind, wrapping));
    }

    public static T WithTextColor<T>(this T component, Color textColor) where T : IWrappable<T>
        => component.WithTextStyle(textColor: textColor);

    public static T WithTextKind<T>(this T component, TextKind textKind) where T : IWrappable<T>
        => component.WithTextStyle(textKind: textKind);

    public static T WithTextWrapping<T>(this T component, TextWrapping wrapping = TextWrapping.Wrap)
        where T : IWrappable<T>
    {
        return component.WithTextStyle(wrapping: wrapping);
    }
}