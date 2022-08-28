using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal abstract class ComponentBase : IComponent
{
    public abstract Size Size { get; }

    public abstract void Accept(IComponentVisitor visitor);

    public IComponent WithStyleApplied(Style style)
        => this.WithStyleAppliedInternal(style);

    public IComponent WrappedInto(Func<IComponent, IComponent> wrapper)
        => this.WrappedIntoInternal(wrapper);
}