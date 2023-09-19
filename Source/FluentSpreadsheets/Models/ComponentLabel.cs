using FluentSpreadsheets.Labels;

namespace FluentSpreadsheets;

internal class ComponentLabel : IComponentIndexLabel, IComponentIndexRangeLabel
{
    private Index? _index;
    private IndexRange? _range;

    public Index Index
    {
        get => _index ?? throw new UnsetLabelException("Index is not set for label");
        set => _index = value;
    }

    public IndexRange Range
    {
        get => _range ?? throw new UnsetLabelException("IndexRange is not set for label");
        set => _range = value;
    }
}