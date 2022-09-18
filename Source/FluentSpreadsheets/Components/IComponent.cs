using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Visitors;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public interface IComponent : IBaseComponent, IWrappable<IComponent>
{
    Size Size { get; }

    void Accept(IComponentVisitor visitor);

    new IComponent WithStyleApplied(Style style);

    new IComponent WrappedInto(Func<IComponent, IComponent> wrapper);
}