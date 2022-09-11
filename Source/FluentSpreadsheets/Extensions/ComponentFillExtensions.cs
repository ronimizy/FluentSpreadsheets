using System.Drawing;
using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public static class ComponentFillExtensions
{
    public static T FilledWith<T>(this T component, Color color) where T : IWrappable<T>
    {
        var style = new Style
        {
            Fill = color,
        };
        
        return component.WithStyleApplied(style);
    }
}