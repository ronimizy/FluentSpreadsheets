using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public interface IHStackComponent : IComponentContainer, IWrappable<IHStackComponent>
{
    new IHStackComponent WithStyleApplied(Style style);

    new IHStackComponent WrappedInto(Func<IComponent, IComponent> wrapper);
}