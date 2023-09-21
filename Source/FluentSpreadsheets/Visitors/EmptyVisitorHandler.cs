using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.Visitors;

// Used for initial render pass, to initialize index labels
public struct EmptyVisitorHandler : IComponentVisitorHandler
{
    public void StyleRange(Style style, IndexRange range) { }

    public void MergeRange(IndexRange range) { }

    public void WriteString(Index index, string value, bool hasFormula) { }

    public void WriteString<T>(Index index, T context, Func<T, string> valueFactory, bool hasFormula) { }

    public void AdjustRows(int from, int upTo) { }

    public void AdjustColumns(int from, int upTo) { }

    public void SetRowHeight(int from, int upTo, RelativeSize height) { }

    public void SetColumnWidth(int from, int upTo, RelativeSize width) { }

    public void FreezeRows(int count) { }

    public void FreezeColumns(int count) { }
}