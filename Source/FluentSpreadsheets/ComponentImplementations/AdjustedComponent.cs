using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class AdjustedComponent : ComponentBase, IRowAdjustedComponent, IColumnAdjustedComponent
{
    private readonly bool _adjustColumn;
    private readonly bool _adjustRow;
    private readonly IComponent _component;

    public AdjustedComponent(IComponent component, bool adjustRow, bool adjustColumn)
    {
        _component = component;
        _adjustRow = adjustRow;
        _adjustColumn = adjustColumn;
    }

    public override Size Size => _component.Size;

    public override void Accept(IComponentVisitor visitor)
    {
        _component.Accept(visitor);

        if (_adjustRow)
            visitor.Visit((IRowAdjustedComponent)this);

        if (_adjustColumn)
            visitor.Visit((IColumnAdjustedComponent)this);
    }
}