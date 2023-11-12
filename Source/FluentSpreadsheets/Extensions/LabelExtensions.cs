using FluentSpreadsheets.Implementations;
using FluentSpreadsheets.Labels;
using FluentSpreadsheets.Labels.Implementations;

namespace FluentSpreadsheets;

public static class LabelExtensions
{
    public static IComponent WithIndexLabel(this IComponent component, out IComponentIndexLabel label)
    {
        var concreteLabel = new ComponentLabel();
        label = concreteLabel;

        return new IndexLabelComponent(component, concreteLabel);
    }

    public static IComponent WithIndexLabel(this IComponent component, IComponentIndexLabelProxy proxy)
    {
        var label = new ComponentLabel();
        proxy.AssignLabel(label);

        return new IndexLabelComponent(component, label);
    }

    public static IComponent WithIndexRangeLabel(this IComponent component, out IComponentIndexRangeLabel label)
    {
        var concreteLabel = new ComponentLabel();
        label = concreteLabel;

        return new IndexLabelComponent(component, concreteLabel);
    }

    public static IComponent WithIndexRangeLabel(this IComponent component, IComponentIndexRangeLabelProxy proxy)
    {
        var label = new ComponentLabel();
        proxy.AssignLabel(label);

        return new IndexLabelComponent(component, label);
    }
}