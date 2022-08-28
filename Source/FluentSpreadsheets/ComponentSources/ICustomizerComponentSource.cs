namespace FluentSpreadsheets;

public interface ICustomizerComponentSource : IComponentSource
{
    IComponent Customize(IComponent componentSource);
}