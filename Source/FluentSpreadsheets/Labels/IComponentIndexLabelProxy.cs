namespace FluentSpreadsheets.Labels;

public interface IComponentIndexLabelProxy
{
    IComponentIndexLabel Label { get; }

    void AssignLabel(IComponentIndexLabel label);
}