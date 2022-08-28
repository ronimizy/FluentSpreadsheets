using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets.Wrappables;

public interface IWrappable<out TSelf>
{
    TSelf WithStyleApplied(Style style);

    TSelf WrappedInto(Func<IComponent, IComponent> wrapper);
}