using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.Implementations;

internal sealed class CellAwareComponent : ComponentBase, ICellAwareComponent
{
    private readonly Func<Index, string> _valueFactory;

    public CellAwareComponent(Func<Index, string> valueFactory, bool hasFormula)
    {
        _valueFactory = valueFactory;
        HasFormula = hasFormula;
    }

    public override Size Size => new Size(1, 1);

    public bool HasFormula { get; }

    public string BuildValue(Index index)
        => _valueFactory.Invoke(index);

    public override void Accept(IComponentVisitor visitor)
        => visitor.Visit(this);
}