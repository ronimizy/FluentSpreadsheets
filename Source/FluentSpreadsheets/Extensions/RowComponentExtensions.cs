namespace FluentSpreadsheets;

public static class RowComponentExtensions
{
    public static IRowComponent CustomizedWith(
        this IRowComponent component,
        Func<IComponent, IComponentSource> customizer)
    {
        return new CustomizerRowComponent(component, customizer);
    }
}