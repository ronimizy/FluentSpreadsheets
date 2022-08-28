using FluentSpreadsheets.ComponentImplementations;
using FluentSpreadsheets.ComponentSourceImplementations;

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
        => new LabelComponent(text);

    public static IComponent Label<T>(T value)
        => Label(value?.ToString() ?? string.Empty);

    public static IComponent Label<T>(T value, string? format, IFormatProvider? formatProvider) where T : IFormattable
        => Label(value.ToString(format, formatProvider));

    public static IComponent Label<T>(T value, IFormatProvider? formatProvider) where T : IFormattable
        => Label(value, null, formatProvider);

    public static IComponent VStack(params IComponentSource[] components)
        => new VStackComponent(components);

    public static IComponent VStack(IEnumerable<IComponentSource> components)
        => new VStackComponent(components);

    public static IComponent VStack(Func<IEnumerable<IComponentSource>> func)
        => new VStackComponent(func.Invoke());

    public static IComponent HStack(params IComponentSource[] components)
        => new HStackComponent(components);

    public static IComponent HStack(IEnumerable<IComponentSource> components)
        => new HStackComponent(components);

    public static IComponent HStack(Func<IEnumerable<IComponentSource>> func)
        => new HStackComponent(func.Invoke());

    public static IComponentSource ForEach<T>(IEnumerable<T> enumerable, Func<T, IComponentSource> factory)
        => new ForEachComponentSource<T>(enumerable, factory);

    public static IRowComponent Row(params IComponentSource[] components)
        => new RowComponent(components);

    public static IRowComponent Row(IEnumerable<IComponentSource> components)
        => new RowComponent(components);

    public static IRowComponent Row(Func<IEnumerable<IComponentSource>> func)
        => new RowComponent(func.Invoke());
}