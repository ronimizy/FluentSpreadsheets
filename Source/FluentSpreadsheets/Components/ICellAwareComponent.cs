namespace FluentSpreadsheets;

public interface ICellAwareComponent : IComponent
{
    bool HasFormula { get; }
    
    string BuildValue(Index index);
}