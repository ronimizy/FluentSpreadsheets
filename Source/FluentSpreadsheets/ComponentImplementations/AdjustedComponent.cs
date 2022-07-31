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

    public async Task AcceptAsync(IComponentVisitor visitor, CancellationToken cancellationToken)
    {
        await _component.AcceptAsync(visitor, cancellationToken);

        if (_adjustRow)
        {
            await visitor.VisitAsync((IRowAdjustedComponent)this, cancellationToken);
        }

        if (_adjustColumn)
        {
            await visitor.VisitAsync((IColumnAdjustedComponent)this, cancellationToken);
        }
    }
}