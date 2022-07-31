using System.Collections.ObjectModel;
using FluentSpreadsheets.Tools;
using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class HStackComponent : IHStackComponent
{
    public HStackComponent(IEnumerable<IComponent> componentEnumerable)
    {
        IComponent[] components = componentEnumerable.ToArray();

        var height = LcmCounter.Count(components.Select(x => x.Size.Height));
        var width = 0;

        for (var i = 0; i < components.Length; i++)
        {
            var size = components[i].Size;
            var scaleFactor = height / size.Height;

            width += size.Width;

            if (scaleFactor is 1)
                continue;

            var scale = new Scale(1, scaleFactor);
            components[i] = new ScaledComponent(components[i], scale);
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

    public Size Size { get; }

    public IReadOnlyCollection<IComponent> Components { get; }

    public Task AcceptAsync(IComponentVisitor visitor, CancellationToken cancellationToken)
        => visitor.VisitAsync(this, cancellationToken);
}