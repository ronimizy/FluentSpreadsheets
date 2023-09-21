namespace FluentSpreadsheets;

public interface IIndexLabelComponent : IComponent
{
    void AssignIndex(Index index);

    void AssignIndexRange(IndexRange range);
}