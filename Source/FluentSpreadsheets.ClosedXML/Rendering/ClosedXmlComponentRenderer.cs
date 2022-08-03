using FluentSpreadsheets.ClosedXML.Visitors;
using FluentSpreadsheets.Rendering;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ClosedXML.Rendering;

public class ClosedXmlComponentRenderer : IComponentRenderer<ClosedXmlRenderCommand>
{
    public Task RenderAsync(ClosedXmlRenderCommand command, CancellationToken cancellationToken = default)
    {
        var handler = new ClosedXmlHandler(command.Worksheet);
        var visitor = new ComponentVisitor<ClosedXmlHandler>(new Index(1, 1), handler);
        command.Component.Accept(visitor);
        
        return Task.CompletedTask;
    }
}