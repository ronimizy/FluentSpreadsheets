using ClosedXML.Excel;
using FluentSpreadsheets.ClosedXML.Handlers;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ClosedXML.Rendering;

internal class ClosedXmlComponentRenderer : IClosedXmlComponentRenderer
{
    public ValueTask RenderAsync(IComponent component, IXLWorksheet worksheet, CancellationToken cancellationToken)
    {
        return RenderAsync(component, worksheet, new Index(1, 1), cancellationToken);
    }

    public ValueTask RenderAsync(IComponent component, IXLWorksheet worksheet, Index startIndex, CancellationToken cancellationToken)
    {
        // Empty handler run is needed to compute labels
        var emptyHandler = new EmptyVisitorHandler();
        var emptyVisitor = new ComponentVisitor<EmptyVisitorHandler>(startIndex, emptyHandler);

        var handler = new ClosedXmlHandler(worksheet);
        var visitor = new ComponentVisitor<ClosedXmlHandler>(startIndex, handler);

        component.Accept(emptyVisitor);
        component.Accept(visitor);

        return default;
    }
}