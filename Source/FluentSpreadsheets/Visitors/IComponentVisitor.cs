namespace FluentSpreadsheets.Visitors;

public interface IComponentVisitor
{
    void Visit(IComponent component);
    
    void Visit(IVStackComponent component);

    void Visit(IHStackComponent component);

    void Visit(ILabelComponent component);

    void Visit(IScaledComponent component);

    void Visit(IStylingComponent component);

    void Visit(IRowAdjustedComponent component);

    void Visit(IColumnAdjustedComponent component);

    void Visit(IRowHeightComponent component);

    void Visit(IColumnWidthComponent component);

    Task ApplyChangesAsync(CancellationToken cancellationToken = default);
}