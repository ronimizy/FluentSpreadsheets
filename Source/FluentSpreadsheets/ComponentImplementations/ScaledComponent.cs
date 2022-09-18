using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal sealed class ScaledComponent : ComponentBase, IScaledComponent
{
    private readonly IComponent _component;

    public ScaledComponent(IComponent component, Scale scale) : base(component.Style)
    {
        _component = component;
        Scale = scale;
    }

    public override Size Size => _component.Size * Scale;

    public Scale Scale { get; }

    public override void Accept(IComponentVisitor visitor)
    {
        visitor.Visit(this);
        _component.Accept(visitor);
    }
}