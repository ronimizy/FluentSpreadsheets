using System.Drawing;
using FluentSpreadsheets.Styles.Text;

namespace FluentSpreadsheets;

public static class ComponentTextExtensions
{
    public static IComponent WithTextStyle(this IComponent component, TextStyle textStyle)
    {
        var style = textStyle.AsStyle();

        return component.WithStyleApplied(style);
    }

    public static IComponent WithTextStyle(
        this IComponent component,
        Color? textColor = null,
        TextKind? textKind = null,
        TextWrapping? wrapping = null)
    {
        return component.WithTextStyle(new TextStyle(textColor, textKind, wrapping));
    }

    public static IComponent WithTextColor(this IComponent component, Color textColor)
        => component.WithTextStyle(textColor: textColor);

    public static IComponent WithTextKind(this IComponent component, TextKind textKind)
        => component.WithTextStyle(textKind: textKind);

    public static IComponent WithTextWrapping(this IComponent component, TextWrapping wrapping = TextWrapping.Wrap)
        => component.WithTextStyle(wrapping: wrapping);
}