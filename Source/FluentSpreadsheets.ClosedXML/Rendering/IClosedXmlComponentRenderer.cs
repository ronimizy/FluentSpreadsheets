using ClosedXML.Excel;

namespace FluentSpreadsheets.ClosedXML.Rendering;

public interface IClosedXmlComponentRenderer
{
    ValueTask RenderAsync(IComponent component, IXLWorksheet worksheet, CancellationToken cancellationToken);

    ValueTask RenderAsync(
        IComponent component,
        IXLWorksheet worksheet,
        Index startIndex,
        CancellationToken cancellationToken);
}