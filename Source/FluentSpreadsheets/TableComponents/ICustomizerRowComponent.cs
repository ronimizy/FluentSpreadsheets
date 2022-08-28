namespace FluentSpreadsheets;

public interface ICustomizerRowComponent : IRowComponent
{
    IComponentSource Customize(IComponent component);
}