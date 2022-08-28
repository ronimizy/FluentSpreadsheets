using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public interface IRowComponent : IWrappable<IRowComponent>, IEnumerable<IBaseComponent> { }