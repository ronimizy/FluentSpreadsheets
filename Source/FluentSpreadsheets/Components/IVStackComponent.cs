namespace FluentSpreadsheets;

public interface IVStackComponent : IComponent
{
    IReadOnlyCollection<IComponent> Components { get; }
}