namespace FluentSpreadsheets.Labels.Implementations;

internal sealed class ComponentLabelProxy : IComponentIndexLabelProxy, IComponentIndexRangeLabelProxy
{
    private IComponentIndexLabel? _label;
    private IComponentIndexRangeLabel? _rangeLabel;

    IComponentIndexLabel IComponentIndexLabelProxy.Label
        => _label ?? throw new UnsetLabelException("Label is not set for proxy");

    IComponentIndexRangeLabel IComponentIndexRangeLabelProxy.Label
        => _rangeLabel ?? throw new UnsetLabelException("Range label is not set for proxy");

    public void AssignLabel(IComponentIndexLabel label)
    {
        _label = label;
    }

    public void AssignLabel(IComponentIndexRangeLabel label)
    {
        _rangeLabel = label;
    }
}