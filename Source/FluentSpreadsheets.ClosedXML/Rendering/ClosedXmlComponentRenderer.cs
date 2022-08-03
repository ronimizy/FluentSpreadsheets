using FluentSpreadsheets.ClosedXML.Visitors;
using FluentSpreadsheets.Rendering;

namespace FluentSpreadsheets.ClosedXML.Rendering;

public class ClosedXmlComponentRenderer : IComponentRenderer<ClosedXmlRenderCommand>
{
    public Task RenderAsync(ClosedXmlRenderCommand command, CancellationToken cancellationToken = default)
    {
        var visitor = new ClosedXmlVisitor(command.Worksheet, new Index(1, 1));
        command.Component.Accept(visitor);
        
        return Task.CompletedTask;
    }
}