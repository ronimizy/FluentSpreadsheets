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

    public  void Accept(IComponentVisitor visitor)
    {
        visitor.Visit(this);
        _component.Accept(visitor);
    }
}