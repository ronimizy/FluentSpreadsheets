using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets;

public interface IComponent
{
    Size Size { get; }

    void Accept(IComponentVisitor visitor);
}