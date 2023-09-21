using FluentSpreadsheets.Labels.Implementations;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.Implementations;

internal class IndexLabelComponent : TopLevelComponentBase, IIndexLabelComponent
{
    private readonly ComponentLabel _label;

    public IndexLabelComponent(IComponent wrapped, ComponentLabel label) : base(wrapped)
    {
        _label = label;
    }

    public override Size Size => Wrapped.Size;

    public override void Accept(IComponentVisitor visitor)
    {
        visitor.Visit(this);
        Wrapped.Accept(visitor);
    }

    protected override IComponent WrapIntoCurrent(IComponent component)
        => new IndexLabelComponent(component, _label);

    public void AssignIndex(Index index)
    {
        _label.Index = index;
    }

    public void AssignIndexRange(IndexRange range)
    {
        _label.Range = range;
    }
}