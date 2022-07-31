using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets;

public interface IComponent
{
    Size Size { get; }

    Task AcceptAsync(IComponentVisitor visitor, CancellationToken cancellationToken = default);
}