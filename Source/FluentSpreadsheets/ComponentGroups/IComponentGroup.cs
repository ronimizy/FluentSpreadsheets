using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public interface IComponentGroup : IBaseComponent, IEnumerable<IBaseComponent>, IWrappable<IComponentGroup>
{
    new IComponentGroup WithStyleApplied(Style style);

    new IComponentGroup WrappedInto(Func<IComponent, IComponent> wrapper);
}