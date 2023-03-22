using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.Visitors;

public interface IComponentVisitorHandler
{
    void StyleRange(Style style, IndexRange range);

    void MergeRange(IndexRange range);

    void WriteString(Index index, string value, bool hasFormula);

    void AdjustRows(int from, int upTo);

    void AdjustColumns(int from, int upTo);

    void SetRowHeight(int from, int upTo, RelativeSize height);

    void SetColumnWidth(int from, int upTo, RelativeSize width);

    void FreezeRows(int count);
    
    void FreezeColumns(int count);
}