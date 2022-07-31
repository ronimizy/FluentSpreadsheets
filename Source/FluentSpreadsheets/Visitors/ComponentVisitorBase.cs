namespace FluentSpreadsheets.Visitors;

public abstract class ComponentVisitorBase : IComponentVisitor
{
    protected Index Index { get; private set; }

    protected Scale Scale { get; private set; }

    protected Style Style { get; private set; }

    protected ComponentVisitorBase(Index index, Style style)
    {
        Index = index;
        Style = style;
        Scale = Scale.None;
    }

    public async Task VisitAsync(IComponent component, CancellationToken cancellationToken = default)
    {
        var size = component.Size * Scale;
        var end = new Index(Index.Row + size.Height, Index.Column + size.Width);
        var range = new IndexRange(Index, end);
        
        if (!Scale.IsNone)
        {
            await MergeRangeAsync(range, cancellationToken);
        }

        await StyleRangeAsync(Style, range, cancellationToken);
    }

    public async Task VisitAsync(IVStackComponent component, CancellationToken cancellationToken)
    {
        var size = component.Size;

        var index = Index;
        var scale = Scale;
        var style = Style;

        var end = new Index(index.Row + size.Height, index.Column + size.Width);
        var range = new IndexRange(Index, end);

        await StyleRangeAsync(style, range, cancellationToken);

        foreach (var subComponent in component.Components)
        {
            var subcomponentSize = subComponent.Size * Scale;

            await subComponent.AcceptAsync(this, cancellationToken);

            Index = index with
            {
                Row = Index.Row + subcomponentSize.Height,
            };

            Scale = scale;
            Style = style;
        }
    }

    public async Task VisitAsync(IHStackComponent component, CancellationToken cancellationToken)
    {
        var size = component.Size;

        var index = Index;
        var scale = Scale;
        var style = Style;

        var end = new Index(index.Row + size.Height, index.Column + size.Width);
        var range = new IndexRange(Index, end);

        await StyleRangeAsync(style, range, cancellationToken);

        foreach (var subComponent in component.Components)
        {
            var subcomponentSize = subComponent.Size * Scale;
            await subComponent.AcceptAsync(this, cancellationToken);

            index = index with
            {
                Column = index.Column + subcomponentSize.Width,
            };

            Index = index;
            Scale = scale;
            Style = style;
        }
    }

    public async Task VisitAsync(ILabelComponent component, CancellationToken cancellationToken)
    {
        var size = component.Size * Scale;
        var end = new Index(Index.Row + size.Height, Index.Column + size.Width);
        var range = new IndexRange(Index, end);

        if (!Scale.IsNone)
        {
            await MergeRangeAsync(range, cancellationToken);
        }

        await StyleRangeAsync(Style, range, cancellationToken);
        await WriteStringAsync(Index, component.Text, cancellationToken);
    }

    public Task VisitAsync(IScaledComponent component, CancellationToken cancellationToken)
    {
        Scale *= component.Scale;
        return Task.CompletedTask;
    }

    public Task VisitAsync(IStylingComponent component, CancellationToken cancellationToken)
    {
        Style = component.TryApply(Style);
        return Task.CompletedTask;
    }

    public Task VisitAsync(IRowAdjustedComponent component, CancellationToken cancellationToken)
    {
        var size = component.Size * Scale;
        AdjustRowsAsync(Index.Row, Index.Row + size.Height, cancellationToken);
        return Task.CompletedTask;
    }

    public Task VisitAsync(IColumnAdjustedComponent component, CancellationToken cancellationToken)
    {
        var size = component.Size * Scale;
        AdjustColumnsAsync(Index.Column, Index.Column + size.Width, cancellationToken);
        return Task.CompletedTask;
    }

    public async Task VisitAsync(IRowHeightComponent component, CancellationToken cancellationToken)
    {
        var size = component.Size * Scale;
        await SetRowHeightAsync(Index.Row, Index.Row + size.Height, component.Height, cancellationToken);
    }

    public Task VisitAsync(IColumnWidthComponent component, CancellationToken cancellationToken)
    {
        var size = component.Size * Scale;
        return SetColumnWidthAsync(Index.Column, Index.Column + size.Width, component.Width, cancellationToken);
    }

    protected abstract Task StyleRangeAsync(Style style, IndexRange range, CancellationToken cancellationToken);

    protected abstract Task MergeRangeAsync(IndexRange range, CancellationToken cancellationToken);

    protected abstract Task WriteStringAsync(Index index, string value, CancellationToken cancellationToken);

    protected abstract Task AdjustRowsAsync(int from, int upTo, CancellationToken cancellationToken);

    protected abstract Task AdjustColumnsAsync(int from, int upTo, CancellationToken cancellationToken);

    protected abstract Task SetRowHeightAsync(int from, int upTo, int height, CancellationToken cancellationToken);

    protected abstract Task SetColumnWidthAsync(int from, int upTo, int width, CancellationToken cancellationToken);
}