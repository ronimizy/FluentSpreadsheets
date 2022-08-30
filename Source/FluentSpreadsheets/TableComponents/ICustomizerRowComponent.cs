namespace FluentSpreadsheets;

public interface ICustomizerRowComponent : IRowComponent
{
    IComponent Customize(IComponent component);
}