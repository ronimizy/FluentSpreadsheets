using System.Collections.ObjectModel;
using FluentSpreadsheets.Tools;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class VStackComponent : IVStackComponent
{
    public VStackComponent(IEnumerable<IComponent> componentEnumerable)
    {
        IComponent[] components = componentEnumerable.ToArray();

        var width = LcmCounter.Count(components.Select(x => x.Size.Width));
        var height = 0;

        for (var i = 0; i < components.Length; i++)
        {
            var size = components[i].Size;
            var scaleFactor = width / size.Width;

            height += size.Height;

            if (scaleFactor is 1)
                continue;

            var scale = new Scale(scaleFactor, 1);
            components[i] = new ScaledComponent(components[i], scale);
        }

        Components = new ReadOnlyCollection<IComponent>(components);
        Size = new Size(width, height);
    }

    /// <summary>
    ///     Constructor used for SheetBuilder to create a stack without redundant memory allocations.
    ///     DO NOT USE ANYWHERE ELSE, EXCEPT WHEN YOU KNOW THAT COMPONENTS ARE WELL SCALED.
    /// </summary>
    public VStackComponent(IReadOnlyCollection<IComponent> components, int width)
    {
        var height = components.Sum(x => x.Size.Height);

        Size = new Size(width, height);
        Components = components;
    }

    public Size Size { get; }

    public IReadOnlyCollection<IComponent> Components { get; }

    public void Accept(IComponentVisitor visitor)
        => visitor.Visit(this);
}