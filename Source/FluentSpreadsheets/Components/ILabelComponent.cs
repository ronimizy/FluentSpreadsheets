namespace FluentSpreadsheets;

public interface ILabelComponent : IComponent
{
    string Text { get; }

    bool HasFormula { get; }
}