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

    public static IComponent WithTextStyle(this IComponent component, Color? textColor, TextKind? textKind)
        => component.WithTextStyle(new TextStyle(textColor, textKind));

    public static IComponent WithTextColor(this IComponent component, Color textColor)
        => component.WithTextStyle(textColor, null);

    public static IComponent WithTextKind(this IComponent component, TextKind textKind)
        => component.WithTextStyle(null, textKind);
}