using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets;

public interface IStylingComponentSource : IComponentSource
{
    Style Style { get; }
    
    IComponentSource StyledComponentSource { get; }
}