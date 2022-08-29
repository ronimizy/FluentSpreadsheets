namespace FluentSpreadsheets;

public interface ICustomizerComponentGroup : IComponentGroup
{
    IComponent Customize(IComponent component);
}