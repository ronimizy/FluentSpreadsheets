using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets;

public interface IStylingComponent : IComponent
{
    IComponent StyledComponent { get; }
    
    Style Style { get; }
}