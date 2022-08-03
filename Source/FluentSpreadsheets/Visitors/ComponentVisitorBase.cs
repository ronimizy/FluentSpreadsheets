namespace FluentSpreadsheets.Visitors;

public abstract class ComponentVisitorBase : IComponentVisitor
{
    protected ComponentVisitorBase(Index index, Style style)
    {
        Index = index;
        Style = style;
        Scale = Scale.None;
    }

    protected Index Index { get; private set; }

    protected Scale Scale { get; private set; }

    protected Style Style { get; private set; }

    public void Visit(IComponent component)
    {
        var size = component.Size * Scale;
        var end = new Index(Index.Row + size.Height, Index.Column + size.Width);
        var range = new IndexRange(Index, end);

        if (!Scale.IsNone)
            MergeRange(range);

        StyleRange(Style, range);
    }

    public void Visit(IVStackComponent component)
    {
        var size = component.Size;

        var index = Index;
        var scale = Scale;
        var style = Style;

        var end = new Index(index.Row + size.Height, index.Column + size.Width);
        var range = new IndexRange(Index, end);

        StyleRange(style, range);

        foreach (var subComponent in component.Components)
        {
            var subcomponentSize = subComponent.Size * Scale;

            subComponent.Accept(this);

            Index = index with
            {
                Row = Index.Row + subcomponentSize.Height,
            };

            Scale = scale;
            Style = style;
        }
    }

    public void Visit(IHStackComponent component)
    {
        var size = component.Size;

        var index = Index;
        var scale = Scale;
        var style = Style;

        var end = new Index(index.Row + size.Height, index.Column + size.Width);
        var range = new IndexRange(Index, end);

        StyleRange(style, range);

        foreach (var subComponent in component.Components)
        {
            var subcomponentSize = subComponent.Size * Scale;
            subComponent.Accept(this);

            index = index with
            {
                Column = index.Column + subcomponentSize.Width,
            };

            Index = index;
            Scale = scale;
            Style = style;
        }
    }

    public void Visit(ILabelComponent component)
    {
        var size = component.Size * Scale;
        var end = new Index(Index.Row + size.Height, Index.Column + size.Width);
        var range = new IndexRange(Index, end);

        if (!Scale.IsNone)
            MergeRange(range);

        StyleRange(Style, range);
        WriteString(Index, component.Text);
    }

    public void Visit(IScaledComponent component)
    {
        Scale *= component.Scale;
    }

    public void Visit(IStylingComponent component)
    {
        Style = component.TryApply(Style);
    }

    public void Visit(IRowAdjustedComponent component)
    {
        var size = component.Size * Scale;
        AdjustRows(Index.Row, Index.Row + size.Height);
    }

    public void Visit(IColumnAdjustedComponent component)
    {
        var size = component.Size * Scale;
        AdjustColumns(Index.Column, Index.Column + size.Width);
    }

    public void Visit(IRowHeightComponent component)
    {
        var size = component.Size * Scale;
        SetRowHeight(Index.Row, Index.Row + size.Height, component.Height);
    }

    public void Visit(IColumnWidthComponent component)
    {
        var size = component.Size * Scale;
        SetColumnWidth(Index.Column, Index.Column + size.Width, component.Width);
    }

    public abstract Task FlushAsync(CancellationToken cancellationToken = default);

    protected abstract void StyleRange(Style style, IndexRange range);

    protected abstract void MergeRange(IndexRange range);

    protected abstract void WriteString(Index index, string value);

    protected abstract void AdjustRows(int from, int upTo);

    protected abstract void AdjustColumns(int from, int upTo);

    protected abstract void SetRowHeight(int from, int upTo, int height);

    protected abstract void SetColumnWidth(int from, int upTo, int width);
}