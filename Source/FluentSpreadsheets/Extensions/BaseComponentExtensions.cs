namespace FluentSpreadsheets;

internal static class BaseComponentExtensions
{
    internal static IEnumerable<IComponent> ExtractComponents(this IEnumerable<IBaseComponent> enumerable)
    {
        foreach (var baseComponent in enumerable)
        {
            if (baseComponent is IComponent component)
            {
                yield return component;
            }
            else if (baseComponent is IComponentGroup source)
            {
                foreach (var innerComponent in source.ExtractComponents())
                {
                    yield return innerComponent;
                }
            }
            else
            {
                throw new UnknownComponentTypeException(baseComponent);
            }
        }
    }
}