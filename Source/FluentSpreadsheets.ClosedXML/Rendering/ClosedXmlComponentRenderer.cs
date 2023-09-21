using FluentSpreadsheets.ClosedXML.Handlers;
using FluentSpreadsheets.Rendering;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ClosedXML.Rendering;

public class ClosedXmlComponentRenderer : IComponentRenderer<ClosedXmlRenderCommand>
{
    public Task RenderAsync(ClosedXmlRenderCommand command, CancellationToken cancellationToken = default)
    {
        var index = new Index(1, 1);

        // Empty handler run is needed to compute labels
        var emptyHandler = new EmptyVisitorHandler();
        var emptyVisitor = new ComponentVisitor<EmptyVisitorHandler>(index, emptyHandler);

        var handler = new ClosedXmlHandler(command.Worksheet);
        var visitor = new ComponentVisitor<ClosedXmlHandler>(index, handler);

        command.Component.Accept(emptyVisitor);
        command.Component.Accept(visitor);

        return Task.CompletedTask;
    }
}