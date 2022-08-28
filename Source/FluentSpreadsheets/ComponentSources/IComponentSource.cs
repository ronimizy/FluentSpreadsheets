using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public interface IComponentSource : IBaseComponent, IEnumerable<IBaseComponent>, IWrappable<IComponentSource> { }