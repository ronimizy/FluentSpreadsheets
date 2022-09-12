using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class FrozenComponent : TopLevelComponentBase, IFrozenRowComponent, IFrozenColumnComponent
{
    private readonly bool _freezeRows;
    private readonly bool _freezeColumns;

    public FrozenComponent(IComponent component, bool freezeRows, bool freezeColumns) : base(component)
    {
        _freezeRows = freezeRows;
        _freezeColumns = freezeColumns;
    }

    public override Size Size => Wrapped.Size;

    public override void Accept(IComponentVisitor visitor)
    {
        Wrapped.Accept(visitor);
        
        if (_freezeRows)
            visitor.Visit((IFrozenRowComponent)this);
        
        if (_freezeColumns)
            visitor.Visit((IFrozenColumnComponent)this);
    }

    protected override IComponent WrapIntoCurrent(IComponent component)
        => new FrozenComponent(component, _freezeRows, _freezeColumns);
}