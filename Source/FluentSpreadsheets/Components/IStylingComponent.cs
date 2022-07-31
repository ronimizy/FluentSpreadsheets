namespace FluentSpreadsheets;

public interface IStylingComponent : IComponent
{
    Style TryApply(Style style);
}