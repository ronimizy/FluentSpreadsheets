using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class AdjustedComponent : IRowAdjustedComponent, IColumnAdjustedComponent
{
    private readonly bool _adjustRow;
    private readonly bool _adjustColumn;
    private readonly IComponent _component;

    public AdjustedComponent(IComponent component, bool adjustRow, bool adjustColumn)
    {
        _component = component;
        _adjustRow = adjustRow;
        _adjustColumn = adjustColumn;
    }

    public Size Size => _component.Size;

    public  void Accept(IComponentVisitor visitor)
    {
         _component.Accept(visitor);

        if (_adjustRow)
        {
             visitor.Visit((IRowAdjustedComponent)this);
        }

        if (_adjustColumn)
        {
             visitor.Visit((IColumnAdjustedComponent)this);
        }
    }
}