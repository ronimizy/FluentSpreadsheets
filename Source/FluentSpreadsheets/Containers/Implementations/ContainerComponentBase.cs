using System.Collections;
using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets.Implementations;

public abstract class ContainerComponentBase<T> : IComponentContainer where T : IComponentContainer
{
    protected ContainerComponentBase(Style style = default)
    {
        Style = style;
    }

    public Style Style { get; }

    public Size Size { get; protected init; }

    public abstract IEnumerator<IComponent> GetEnumerator();

    public abstract void Accept(IComponentVisitor visitor);

    protected abstract T WrappedIntoProtected(Func<IComponent, IComponent> wrapper);

    protected abstract T WithStyleAppliedProtected(Style style);

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    IComponent IComponent.WrappedInto(Func<IComponent, IComponent> wrapper)
        => WrappedIntoProtected(wrapper);

    IComponent IWrappable<IComponent>.WrappedInto(Func<IComponent, IComponent> wrapper)
        => WrappedIntoProtected(wrapper);

    IBaseComponent IWrappable<IBaseComponent>.WrappedInto(Func<IComponent, IComponent> wrapper)
        => WrappedIntoProtected(wrapper);

    IComponentContainer IComponentContainer.WrappedInto(Func<IComponent, IComponent> wrapper)
        => WrappedIntoProtected(wrapper);

    IComponentContainer IWrappable<IComponentContainer>.WrappedInto(Func<IComponent, IComponent> wrapper)
        => WrappedIntoProtected(wrapper);

    IComponent IComponent.WithStyleApplied(Style style)
        => WithStyleAppliedProtected(style);

    IComponent IWrappable<IComponent>.WithStyleApplied(Style style)
        => WithStyleAppliedProtected(style);

    IBaseComponent IWrappable<IBaseComponent>.WithStyleApplied(Style style)
        => WithStyleAppliedProtected(style);

    IComponentContainer IComponentContainer.WithStyleApplied(Style style)
        => WithStyleAppliedProtected(style);

    IComponentContainer IWrappable<IComponentContainer>.WithStyleApplied(Style style)
        => WithStyleAppliedProtected(style);
}