using FluentSpreadsheets.Tools;
using static FluentSpreadsheets.ComponentFactory;

namespace FluentSpreadsheets.TableScaling;

internal abstract class Scaler
{
    public IEnumerable<IEnumerable<IComponent>> Scale(IEnumerable<IEnumerable<IBaseComponent>> componentSources)
    {
        EnumeratorNode<IBaseComponent>[] nodes = componentSources
            .Select(x => new EnumeratorNode<IBaseComponent>(x.GetEnumerator()))
            .ToArray();

        try
        {
            while (nodes.Any(x => x.HasValues))
            {
                FlushDepletedEnumeratorNodes(nodes);
                MoveEnumeratorNodesEdgeEnumerators(nodes);
                IterateEnumeratorNodeInDepth(nodes);
                ScaleEnumeratorNodesEdgeComponents(nodes);
            }

            FlushDepletedEnumeratorNodes(nodes);
            return nodes.Select(x => x.Values.OfType<IComponent>());
        }
        finally
        {
            foreach (EnumeratorNode<IBaseComponent> node in nodes)
            {
                node.Dispose();
            }
        }
    }

    private static void MoveEnumeratorNodesEdgeEnumerators<T>(IEnumerable<EnumeratorNode<T>> nodes)
    {
        foreach (EnumeratorNode<T> node in nodes.Where(x => x.HasValues))
        {
            node.MoveNext();
        }
    }

    protected abstract void ValidateCustomization(IComponent component, IComponent customizedComponent);

    protected abstract Axis GetAxis();

    protected abstract IComponent MergeComponents(IEnumerable<IBaseComponent> componentSources);

    protected abstract int SelectDimension(IComponent component);

    private static void IterateEnumeratorNodeInDepth(IList<EnumeratorNode<IBaseComponent>> nodes)
    {
        for (var i = 0; i < nodes.Count; i++)
        {
            if (!nodes[i].HasValues)
                continue;

            while (nodes[i].Value is IComponentSource componentSource)
            {
                EnumeratorNode<IBaseComponent> node = nodes[i];
                IEnumerator<IBaseComponent> enumerator = componentSource.GetEnumerator();
                nodes[i] = new EnumeratorNode<IBaseComponent>(enumerator, node);
                nodes[i].MoveNext();
            }
        }
    }

    private void ScaleEnumeratorNodesEdgeComponents(EnumeratorNode<IBaseComponent>[] nodes)
    {
        IEnumerable<int> dimensions = nodes
            .Where(x => x.HasValues)
            .Select(x => x.Value)
            .OfType<IComponent>()
            .Select(SelectDimension);

        var dimension = LcmCounter.Count(dimensions);

        foreach (EnumeratorNode<IBaseComponent> enumerator in nodes.Where(x => x.HasValues))
        {
            if (enumerator.Value is not IComponent component)
                throw new InvalidOperationException("Max depth component source must be a component");

            var factor = dimension / component.Size.Width;
            var value = component;

            if (factor is not 1)
            {
                value = component.ScaledBy(factor, GetAxis());
            }

            enumerator.Values.Add(value);
        }
    }

    private void FlushDepletedEnumeratorNodes(IList<EnumeratorNode<IBaseComponent>> nodes)
    {
        for (var i = 0; i < nodes.Count; i++)
        {
            while (!nodes[i].HasValues && nodes[i].Next is not null)
            {
                EnumeratorNode<IBaseComponent> node = nodes[i];
                node.Dispose();

                if (node.Next?.Value is ICustomizerComponentSource customizerComponentSource)
                {
                    IComponent component = node.Values.Count switch
                    {
                        0 => Empty(),
                        1 => (IComponent)node.Values[0],
                        _ => MergeComponents(node.Values),
                    };

                    var customizedComponent = customizerComponentSource.Customize(component);
                    ValidateCustomization(component, customizedComponent);
                    nodes[i].Next!.Values.Add(customizedComponent);
                }
                else
                {
                    nodes[i].Next!.Values.AddRange(nodes[i].Values);
                }

                nodes[i] = nodes[i].Next!;
            }
        }
    }
}