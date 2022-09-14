using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.Visitors;

public class ComponentVisitor<THandler> : IComponentVisitor where THandler : IComponentVisitorHandler
{
    private readonly THandler _handler;
    private Index _index;
    private Scale _scale;
    private Style _style;

    public ComponentVisitor(Index index, THandler handler, Style style = default)
    {
        _index = index;
        _style = style;
        _handler = handler;
        _scale = Scale.None;
    }

    public void Visit(IComponent component)
    {
        var size = component.Size * _scale;
        var end = new Index(_index.Row + size.Height, _index.Column + size.Width);
        var range = new IndexRange(_index, end);

        if (!_scale.IsNone)
            _handler.MergeRange(range);

        _handler.StyleRange(_style, range);
    }

    public void Visit(IVStackComponent component)
    {
        var size = component.Size;

        var index = _index;
        var scale = _scale;
        var style = _style;

        var end = new Index(index.Row + size.Height, index.Column + size.Width);
        var range = new IndexRange(_index, end);

        _handler.StyleRange(style, range);

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

        _handler.StyleRange(style, range);

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
            _handler.MergeRange(range);

        _handler.StyleRange(_style, range);
        _handler.WriteString(_index, component.Text);
    }

    public void Visit(ICellAwareComponent component)
    {
        var size = component.Size * _scale;
        var end = new Index(_index.Row + size.Height, _index.Column + size.Width);
        var range = new IndexRange(_index, end);

        if (!_scale.IsNone)
            _handler.MergeRange(range);

        var text = component.BuildValue(_index);
        _handler.StyleRange(_style, range);
        _handler.WriteString(_index, text);
    }

    public void Visit(IScaledComponent component)
    {
        _scale *= component.Scale;
    }

    public void Visit(IStylingComponent component)
    {
        _style = _style.Apply(component.Style);
    }

    public void Visit(IRowAdjustedComponent component)
    {
        var size = component.Size * _scale;
        _handler.AdjustRows(_index.Row, _index.Row + size.Height);
    }

    public void Visit(IColumnAdjustedComponent component)
    {
        var size = component.Size * _scale;
        _handler.AdjustColumns(_index.Column, _index.Column + size.Width);
    }

    public void Visit(IRowHeightComponent component)
    {
        var size = component.Size * _scale;
        _handler.SetRowHeight(_index.Row, _index.Row + size.Height, component.Height);
    }

    public void Visit(IColumnWidthComponent component)
    {
        var size = component.Size * _scale;
        _handler.SetColumnWidth(_index.Column, _index.Column + size.Width, component.Width);
    }

    public void Visit(IFrozenRowComponent component)
    {
        var count = _index.Row + component.Size.Height - 1;
        _handler.FreezeRows(count);
    }

    public void Visit(IFrozenColumnComponent component)
    {
        var count = _index.Column + component.Size.Width - 1;
        _handler.FreezeColumns(count);
    }
}