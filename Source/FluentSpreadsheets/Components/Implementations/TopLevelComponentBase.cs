using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.Implementations;

internal abstract class TopLevelComponentBase : ComponentBase
{
    protected TopLevelComponentBase(IComponent wrapped) : base(wrapped.Style)
    {
        Wrapped = wrapped;
    }

    protected TopLevelComponentBase(IComponent wrapped, Style style) : base(style)
    {
        Wrapped = wrapped;
    }

    protected IComponent Wrapped { get; }

    public override IComponent WrappedInto(Func<IComponent, IComponent> wrapper)
    {
        var wrapped = Wrapped.WrappedInto(wrapper);
        return WrapIntoCurrent(wrapped);
    }

    protected abstract IComponent WrapIntoCurrent(IComponent component);
}