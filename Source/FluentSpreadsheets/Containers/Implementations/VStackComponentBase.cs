using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.Implementations;

internal abstract class VStackComponentBase : ContainerComponentBase<IVStackComponent>, IVStackComponent
{
    protected VStackComponentBase(Style style = default) : base(style) { }

    public override void Accept(IComponentVisitor visitor)
        => visitor.Visit(this);

    public IVStackComponent WrappedInto(Func<IComponent, IComponent> wrapper)
        => new ModifierVStackComponent(this, wrapper);

    public IVStackComponent WithStyleApplied(Style style)
        => new StyledVStackComponent(this, style);

    protected override IVStackComponent WrappedIntoProtected(Func<IComponent, IComponent> wrapper)
        => WrappedInto(wrapper);

    protected override IVStackComponent WithStyleAppliedProtected(Style style)
        => WithStyleApplied(style);

    IVStackComponent IVStackComponent.WrappedInto(Func<IComponent, IComponent> wrapper)
        => WrappedIntoProtected(wrapper);

    IVStackComponent IVStackComponent.WithStyleApplied(Style style)
        => WithStyleAppliedProtected(style);
}