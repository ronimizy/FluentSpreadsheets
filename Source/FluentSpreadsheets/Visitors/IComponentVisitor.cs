namespace FluentSpreadsheets.Visitors;

public interface IComponentVisitor
{
    Task VisitAsync(IComponent component, CancellationToken cancellationToken = default);
    
    Task VisitAsync(IVStackComponent component, CancellationToken cancellationToken = default);

    Task VisitAsync(IHStackComponent component, CancellationToken cancellationToken = default);

    Task VisitAsync(ILabelComponent component, CancellationToken cancellationToken = default);

    Task VisitAsync(IScaledComponent component, CancellationToken cancellationToken = default);

    Task VisitAsync(IStylingComponent component, CancellationToken cancellationToken = default);

    Task VisitAsync(IRowAdjustedComponent component, CancellationToken cancellationToken = default);

    Task VisitAsync(IColumnAdjustedComponent component, CancellationToken cancellationToken = default);

    Task VisitAsync(IRowHeightComponent component, CancellationToken cancellationToken = default);

    Task VisitAsync(IColumnWidthComponent component, CancellationToken cancellationToken = default);
}