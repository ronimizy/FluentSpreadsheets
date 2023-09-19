using FluentSpreadsheets.ComponentImplementations;
using FluentSpreadsheets.Labels;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public static class LabelExtensions
{
    public static T WithIndexLabel<T>(this T component, out IComponentIndexLabel label) where T : IWrappable<T>
    {
        var concreteLabel = new ComponentLabel();
        label = concreteLabel;

        return component.WrappedInto(c => new IndexLabelComponent(c, concreteLabel));
    }

    public static T WithIndexRangeLabel<T>(this T component, out IComponentIndexRangeLabel label)
        where T : IWrappable<T>
    {
        var concreteLabel = new ComponentLabel();
        label = concreteLabel;

        return component.WrappedInto(c => new IndexLabelComponent(c, concreteLabel));
    }
}