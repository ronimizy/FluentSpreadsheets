using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public interface IComponentContainer : IComponent, IEnumerable<IComponent>, IWrappable<IComponentContainer>
{
    new IComponentContainer WithStyleApplied(Style style);

    new IComponentContainer WrappedInto(Func<IComponent, IComponent> wrapper);
}