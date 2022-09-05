using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class CellAwareComponent : ComponentBase, ICellAwareComponent
{
    private readonly Func<Index, string> _valueFactory;

    public CellAwareComponent(Func<Index, string> valueFactory)
    {
        _valueFactory = valueFactory;
    }

    public override Size Size => new Size(1, 1);

    public string BuildValue(Index index)
        => _valueFactory.Invoke(index);

    public override void Accept(IComponentVisitor visitor)
        => visitor.Visit(this);
}