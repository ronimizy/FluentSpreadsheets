using FluentSpreadsheets.TableScaling;
using static FluentSpreadsheets.ComponentFactory;

namespace FluentSpreadsheets.Tables;

public abstract class RowTable<T> : ITable<T>
{
    public IComponent Render(T model)
    {
        IRowComponent[] rowComponents = RenderRows(model).ToArray();
        IEnumerable<IEnumerable<IComponent>> scaled = RowTableScaler.Instance.Scale(rowComponents);

        IEnumerable<IComponentSource> rows = scaled.Select(HStack).Select((c, i) =>
        {
            var row = rowComponents[i];

            if (row is ICustomizerRowComponent customizer)
                return customizer.Customize(c);

            return c;
        });

        var table = VStack(rows);

        // ReSharper disable once SuspiciousTypeConversion.Global
        if (this is ITableCustomizer tableCustomizer)
        {
            var customizedTable = tableCustomizer.Customize(table);
            
            if (customizedTable is not IComponent component)
                throw new InvalidOperationException("Customized table must be a component.");

            table = component;
        }

        return table;
    }

    protected abstract IEnumerable<IRowComponent> RenderRows(T model);
}