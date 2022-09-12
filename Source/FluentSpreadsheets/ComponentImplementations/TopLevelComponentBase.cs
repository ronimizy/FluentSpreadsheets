using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal abstract class TopLevelComponentBase : IComponent
{
    protected TopLevelComponentBase(IComponent wrapped)
    {
        Wrapped = wrapped;
    }

    protected IComponent Wrapped { get; }

    public abstract Size Size { get; }

    public IComponent WithStyleApplied(Style style)
        => this.WithStyleAppliedInternal(style);

    public IComponent WrappedInto(Func<IComponent, IComponent> wrapper)
    {
        var wrapped = Wrapped.WrappedInto(wrapper);
        return WrapIntoCurrent(wrapped);
    }

    public abstract void Accept(IComponentVisitor visitor);

    protected abstract IComponent WrapIntoCurrent(IComponent component);
}