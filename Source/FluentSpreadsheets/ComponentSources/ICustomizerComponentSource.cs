namespace FluentSpreadsheets;

public interface ICustomizerComponentSource : IComponentSource
{
    IComponentSource Customize(IComponentSource componentSource);
}