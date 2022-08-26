using FluentSpreadsheets.ComponentImplementations;
using FluentSpreadsheets.Styles;

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

    public static IComponent VStack(params IComponent[] components)
        => new VStackComponent(components);

    public static IComponent VStack(IEnumerable<IComponent> components)
        => new VStackComponent(components);

    public static IComponent VStack(Func<IEnumerable<IComponent>> func)
        => new VStackComponent(func.Invoke());

    public static IComponent HStack(params IComponent[] components)
        => new HStackComponent(components);

    public static IComponent HStack(IEnumerable<IComponent> components)
        => new HStackComponent(components);

    public static IComponent HStack(Func<IEnumerable<IComponent>> func)
        => new HStackComponent(func.Invoke());
}