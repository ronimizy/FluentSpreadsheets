using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal sealed class AdjustedComponent : TopLevelComponentBase, IRowAdjustedComponent, IColumnAdjustedComponent
{
    private readonly bool _adjustColumn;
    private readonly bool _adjustRow;

    public AdjustedComponent(IComponent component, bool adjustRow, bool adjustColumn) : base(component)
    {
        _adjustRow = adjustRow;
        _adjustColumn = adjustColumn;
    }

    public override Size Size => Wrapped.Size;

    public override void Accept(IComponentVisitor visitor)
    {
        Wrapped.Accept(visitor);

        if (_adjustRow)
            visitor.Visit((IRowAdjustedComponent)this);

        if (_adjustColumn)
            visitor.Visit((IColumnAdjustedComponent)this);
    }

    protected override IComponent WrapIntoCurrent(IComponent component)
        => new AdjustedComponent(component, _adjustRow, _adjustColumn);
}