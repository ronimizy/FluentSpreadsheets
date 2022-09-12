using System.Collections.ObjectModel;
using FluentSpreadsheets.Tools;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class HStackComponent : ComponentBase, IHStackComponent
{
    public HStackComponent(IEnumerable<IBaseComponent> componentEnumerable)
    {
        IComponent[] components = componentEnumerable.ExtractComponents().ToArray();

        var height = LcmCounter.Count(components.Select(x => x.Size.Height));
        var width = 0;

        for (var i = 0; i < components.Length; i++)
        {
            var size = components[i].Size;
            var scaleFactor = height / size.Height;

            width += size.Width;

            if (scaleFactor is 1)
                continue;

            components[i] = components[i].ScaledBy(scaleFactor, Axis.Vertical);
        }

        Components = new ReadOnlyCollection<IComponent>(components);
        Size = new Size(width, height);
    }

    /// <summary>
    ///     Constructor used for SheetBuilder to create a stack without redundant memory allocations.
    ///     DO NOT USE ANYWHERE ELSE, EXCEPT WHEN YOU KNOW THAT COMPONENTS ARE WELL SCALED.
    /// </summary>
    public HStackComponent(IReadOnlyCollection<IComponent> components, int height)
    {
        var width = components.Sum(x => x.Size.Width);

        Size = new Size(width, height);
        Components = components;
    }

    public override Size Size { get; }

    public IReadOnlyCollection<IComponent> Components { get; }

    public override void Accept(IComponentVisitor visitor)
        => visitor.Visit(this);
}