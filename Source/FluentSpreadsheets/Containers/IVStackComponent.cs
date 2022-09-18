using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public interface IVStackComponent : IComponentContainer, IWrappable<IVStackComponent>
{
    new IVStackComponent WithStyleApplied(Style style);

    new IVStackComponent WrappedInto(Func<IComponent, IComponent> wrapper);
}