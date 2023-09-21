using FluentSpreadsheets.Implementations;
using FluentSpreadsheets.Labels;
using FluentSpreadsheets.Labels.Implementations;
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

    public static T WithIndexLabel<T>(this T component, IComponentIndexLabelProxy proxy) where T : IWrappable<T>
    {
        var label = new ComponentLabel();
        proxy.AssignLabel(label);

        return component.WrappedInto(c => new IndexLabelComponent(c, label));
    }

    public static T WithIndexRangeLabel<T>(this T component, out IComponentIndexRangeLabel label)
        where T : IWrappable<T>
    {
        var concreteLabel = new ComponentLabel();
        label = concreteLabel;

        return component.WrappedInto(c => new IndexLabelComponent(c, concreteLabel));
    }

    public static T WithIndexRangeLabel<T>(this T component, IComponentIndexRangeLabelProxy proxy)
        where T : IWrappable<T>
    {
        var label = new ComponentLabel();
        proxy.AssignLabel(label);

        return component.WrappedInto(c => new IndexLabelComponent(c, label));
    }
}