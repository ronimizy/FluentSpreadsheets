namespace FluentSpreadsheets;

public interface ICellAwareComponent : IComponent
{
    string BuildValue(Index index);
}