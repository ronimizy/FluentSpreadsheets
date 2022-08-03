namespace FluentSpreadsheets.Rendering;

public interface IComponentRenderer<in TCommand> where TCommand : IRenderCommand
{
    Task RenderAsync(TCommand command, CancellationToken cancellationToken = default);
}