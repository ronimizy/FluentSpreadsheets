namespace FluentSpreadsheets.Labels;

public interface IComponentIndexRangeLabelProxy
{
    IComponentIndexRangeLabel Label { get; }

    void AssignLabel(IComponentIndexRangeLabel label);
}