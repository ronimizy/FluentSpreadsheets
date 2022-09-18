using FluentSpreadsheets.Styles;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public interface IBaseComponent : IWrappable<IBaseComponent>
{
    Style Style { get; }
}