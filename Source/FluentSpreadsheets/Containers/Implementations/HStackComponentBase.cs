using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.Implementations;

internal abstract class HStackComponentBase : ContainerComponentBase<IHStackComponent>, IHStackComponent
{
    protected HStackComponentBase(Style style = default) : base(style) { }

    public override void Accept(IComponentVisitor visitor)
        => visitor.Visit(this);

    public virtual IHStackComponent WrappedInto(Func<IComponent, IComponent> wrapper)
        => new ModifierHStackComponent(this, wrapper);

    public virtual IHStackComponent WithStyleApplied(Style style)
        => new StyledHStackComponent(this, style);

    protected override IHStackComponent WrappedIntoProtected(Func<IComponent, IComponent> wrapper)
        => WrappedInto(wrapper);

    protected override IHStackComponent WithStyleAppliedProtected(Style style)
        => WithStyleApplied(style);

    IHStackComponent IHStackComponent.WrappedInto(Func<IComponent, IComponent> wrapper)
        => WrappedInto(wrapper);

    IHStackComponent IHStackComponent.WithStyleApplied(Style style)
        => WithStyleApplied(style);
}