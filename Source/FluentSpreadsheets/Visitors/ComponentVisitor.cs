namespace FluentSpreadsheets.Visitors;

public class ComponentVisitor<THandler> : IComponentVisitor where THandler : IComponentVisitorHandler
{
    public readonly THandler Handler;
    private Index _index;
    private Scale _scale;
    private Style _style;

    public ComponentVisitor(Index index, THandler handler, Style style = default)
    {
        _index = index;
        _style = style;
        Handler = handler;
        _scale = Scale.None;
    }

    public void Visit(IComponent component)
    {
        var size = component.Size * _scale;
        var end = new Index(_index.Row + size.Height, _index.Column + size.Width);
        var range = new IndexRange(_index, end);

        if (!_scale.IsNone)
            Handler.MergeRange(range);

        Handler.StyleRange(_style, range);
    }

    public void Visit(IVStackComponent component)
    {
        var size = component.Size;

        var index = _index;
        var scale = _scale;
        var style = _style;

        var end = new Index(index.Row + size.Height, index.Column + size.Width);
        var range = new IndexRange(_index, end);

        Handler.StyleRange(style, range);

        foreach (var subComponent in component.Components)
        {
            var subcomponentSize = subComponent.Size * _scale;

            subComponent.Accept(this);

            _index = index with
            {
                Row = _index.Row + subcomponentSize.Height,
            };

            _scale = scale;
            _style = style;
        }
    }

    public void Visit(IHStackComponent component)
    {
        var size = component.Size;

        var index = _index;
        var scale = _scale;
        var style = _style;

        var end = new Index(index.Row + size.Height, index.Column + size.Width);
        var range = new IndexRange(_index, end);

        Handler.StyleRange(style, range);

        foreach (var subComponent in component.Components)
        {
            var subcomponentSize = subComponent.Size * _scale;
            subComponent.Accept(this);

            index = index with
            {
                Column = index.Column + subcomponentSize.Width,
            };

            _index = index;
            _scale = scale;
            _style = style;
        }
    }

    public void Visit(ILabelComponent component)
    {
        var size = component.Size * _scale;
        var end = new Index(_index.Row + size.Height, _index.Column + size.Width);
        var range = new IndexRange(_index, end);

        if (!_scale.IsNone)
            Handler.MergeRange(range);

        Handler.StyleRange(_style, range);
        Handler.WriteString(_index, component.Text);
    }

    public void Visit(IScaledComponent component)
    {
        _scale *= component.Scale;
    }

    public void Visit(IStylingComponent component)
    {
        _style = component.TryApply(_style);
    }

    public void Visit(IRowAdjustedComponent component)
    {
        var size = component.Size * _scale;
        Handler.AdjustRows(_index.Row, _index.Row + size.Height);
    }

    public void Visit(IColumnAdjustedComponent component)
    {
        var size = component.Size * _scale;
        Handler.AdjustColumns(_index.Column, _index.Column + size.Width);
    }

    public void Visit(IRowHeightComponent component)
    {
        var size = component.Size * _scale;
        Handler.SetRowHeight(_index.Row, _index.Row + size.Height, component.Height);
    }

    public void Visit(IColumnWidthComponent component)
    {
        var size = component.Size * _scale;
        Handler.SetColumnWidth(_index.Column, _index.Column + size.Width, component.Width);
    }
}