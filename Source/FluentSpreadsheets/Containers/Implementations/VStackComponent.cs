using System.Collections.ObjectModel;
using FluentSpreadsheets.Tools;

namespace FluentSpreadsheets.Implementations;

internal sealed class VStackComponent : VStackComponentBase
{
    private readonly IReadOnlyCollection<IComponent> _components;

    public VStackComponent(IEnumerable<IBaseComponent> componentEnumerable)
    {
        IComponent[] components = componentEnumerable.ExtractComponents().ToArray();

        var width = LcmCounter.Count(components.Select(x => x.Size.Width));
        var height = 0;

        for (var i = 0; i < components.Length; i++)
        {
            var size = components[i].Size;
            var scaleFactor = width / size.Width;

            height += size.Height;

            if (scaleFactor is 1)
                continue;

            components[i] = components[i].ScaledBy(scaleFactor, Axis.Horizontal);
        }

        _components = new ReadOnlyCollection<IComponent>(components);
        Size = new Size(width, height);
    }

    /// <summary>
    ///     Constructor used for SheetBuilder to create a stack without redundant memory allocations.
    ///     DO NOT USE ANYWHERE ELSE, EXCEPT WHEN YOU KNOW THAT COMPONENTS ARE WELL SCALED.
    /// </summary>
    internal VStackComponent(IReadOnlyCollection<IComponent> components, int width)
    {
        var height = components.Sum(x => x.Size.Height);

        Size = new Size(width, height);
        _components = components;
    }

    public override IEnumerator<IComponent> GetEnumerator()
        => _components.GetEnumerator();
}