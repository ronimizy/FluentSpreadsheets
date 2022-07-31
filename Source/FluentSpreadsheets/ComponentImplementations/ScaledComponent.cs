using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class ScaledComponent : IScaledComponent
{
    private readonly IComponent _component;

    public ScaledComponent(IComponent component, Scale scale)
    {
        _component = component;
        Scale = scale;
    }

    public Size Size => _component.Size * Scale;

    public Scale Scale { get; }

    public async Task AcceptAsync(IComponentVisitor visitor, CancellationToken cancellationToken)
    {
        await visitor.VisitAsync(this, cancellationToken);
        await _component.AcceptAsync(visitor, cancellationToken);
    }
}