using FluentSpreadsheets.ContainerImplementations;
using FluentSpreadsheets.TableScaling;
using static FluentSpreadsheets.ComponentFactory;

namespace FluentSpreadsheets.Tables;

public abstract class RowTable<T> : ITable<T>
{
    public IComponent Render(T model)
    {
        IRowComponent[] rowComponents = RenderRows(model).ToArray();
        IEnumerable<IEnumerable<IComponent>> scaled = RowTableScaler.Instance.Scale(rowComponents);
        IComponent[] rows = scaled.Select(HStack).ToArray();

        var width = rows.Length is 0 ? 0 : rows.Max(x => x.Size.Width);
        var stack = new VStackComponent(rows, width);

        return Customize(stack);
    }

    protected abstract IEnumerable<IRowComponent> RenderRows(T model);

    protected virtual IComponent Customize(IComponent component)
        => component;
}