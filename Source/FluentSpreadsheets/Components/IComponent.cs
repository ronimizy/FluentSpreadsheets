using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets;

public interface IComponent : IComponentSource
{
    Size Size { get; }

    void Accept(IComponentVisitor visitor);
}