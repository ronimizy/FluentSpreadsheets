using FluentSpreadsheets.TableScaling;
using static FluentSpreadsheets.ComponentFactory;

namespace FluentSpreadsheets.Tables;

public abstract class RowTable<T> : ITable<T>
{
    public IComponent Render(T model)
    {
        IRowComponent[] rowComponents = RenderRows(model).ToArray();
        IEnumerable<IEnumerable<IComponent>> scaled = RowTableScaler.Instance.Scale(rowComponents);

        IEnumerable<IComponent> rows = scaled.Select(HStack).Select((c, i) =>
        {
            var row = rowComponents[i];

            if (row is ICustomizerRowComponent customizer)
                return customizer.Customize(c);

            return c;
        });

        var table = VStack(rows);

        if (this is ITableCustomizer tableCustomizer)
            table = tableCustomizer.Customize(table);

        return table;
    }

    protected abstract IEnumerable<IRowComponent> RenderRows(T model);
}