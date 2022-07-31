namespace FluentSpreadsheets;

public interface IHStackComponent : IComponent
{
    IReadOnlyCollection<IComponent> Components { get; }
}