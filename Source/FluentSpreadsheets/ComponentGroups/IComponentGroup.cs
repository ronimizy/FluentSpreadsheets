using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public interface IComponentGroup : IBaseComponent, IEnumerable<IBaseComponent>, IWrappable<IComponentGroup> { }