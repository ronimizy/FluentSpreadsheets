using FluentSpreadsheets.Implementations;
using FluentSpreadsheets.Tables;

namespace FluentSpreadsheets;

public static class ComponentFactory
{
    public static IComponent None()
        => new EmptyComponent(default);

    public static IComponent Empty(Size size)
        => new EmptyComponent(size);

    public static IComponent Empty()
        => Empty(new Size(1, 1));

    public static IComponent Label(string text)
        => new LabelComponent(text, false);

    public static IComponent Label()
        => Label(string.Empty);

    public static IComponent Label<T>(T value)
        => Label(value?.ToString() ?? string.Empty);

    public static IComponent Label<T>(T value, string? format, IFormatProvider? formatProvider) where T : IFormattable
        => Label(value.ToString(format, formatProvider));

    public static IComponent Label<T>(T value, IFormatProvider? formatProvider) where T : IFormattable
        => Label(value, null, formatProvider);

    public static IComponent Label(Func<Index, string> factory)
        => new CellAwareComponent(factory, false);

    public static IComponent Label<T>(Func<Index, T> factory)
        => new CellAwareComponent(x => factory.Invoke(x)?.ToString() ?? string.Empty, false);

    public static IComponent Label<T>(Func<Index, T> factory, string? format, IFormatProvider? formatProvider)
        where T : IFormattable
    {
        string Func(Index x)
            => factory.Invoke(x)?.ToString(format, formatProvider) ?? string.Empty;

        return new CellAwareComponent(Func, false);
    }

    public static IComponent Label<T>(Func<Index, T> factory, IFormatProvider? formatProvider) where T : IFormattable
        => Label(factory, null, formatProvider);

    public static IComponent Formula(string formula)
        => new LabelComponent(formula, true);

    public static IComponent Formula(Func<Index, string> factory)
        => new CellAwareComponent(factory, true);

    public static IComponent VStack(params IBaseComponent[] components)
        => new VStackComponent(components);

    public static IComponent VStack(IEnumerable<IBaseComponent> components)
        => new VStackComponent(components);

    public static IComponent VStack(Func<IEnumerable<IBaseComponent>> func)
        => new VStackComponent(func.Invoke());
    
    public static IComponent VStack(IComponentGroup group)
        => new VStackComponent(group);

    public static IComponent HStack(params IBaseComponent[] components)
        => new HStackComponent(components);

    public static IComponent HStack(IEnumerable<IBaseComponent> components)
        => new HStackComponent(components);

    public static IComponent HStack(IComponentGroup group)
        => new HStackComponent(group);

    public static IComponent HStack(Func<IEnumerable<IBaseComponent>> func)
        => new HStackComponent(func.Invoke());

    public static IComponentGroup ForEach<T>(IEnumerable<T> enumerable, Func<T, IBaseComponent> factory)
        => new ForEachComponentGroup<T>(enumerable, factory);

    public static IRowComponent Row(params IBaseComponent[] components)
        => new RowComponent(components);

    public static IRowComponent Row(IEnumerable<IBaseComponent> components)
        => new RowComponent(components);

    public static IRowComponent Row(Func<IEnumerable<IBaseComponent>> func)
        => new RowComponent(func.Invoke());

    public static ITable<T> Table<T>(Func<T, IEnumerable<IRowComponent>> generator)
        => new DynamicTable<T>(generator);
}