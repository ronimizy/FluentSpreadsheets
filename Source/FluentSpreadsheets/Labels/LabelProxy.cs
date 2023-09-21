using FluentSpreadsheets.Labels.Implementations;

namespace FluentSpreadsheets.Labels;

public static class LabelProxy
{
    public static IComponentIndexLabelProxy Create()
    {
        return new ComponentLabelProxy();
    }

    public static IComponentIndexRangeLabelProxy CreateForRange()
    {
        return new ComponentLabelProxy();
    }
}