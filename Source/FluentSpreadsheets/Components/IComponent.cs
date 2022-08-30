using FluentSpreadsheets.Visitors;
using FluentSpreadsheets.Wrappables;

namespace FluentSpreadsheets;

public interface IComponent : IBaseComponent, IWrappable<IComponent>
{
    Size Size { get; }

    void Accept(IComponentVisitor visitor);
}