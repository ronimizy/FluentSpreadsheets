using FluentSpreadsheets.Tools;
using static FluentSpreadsheets.ComponentFactory;

namespace FluentSpreadsheets.TableScaling;

internal abstract class Scaler
{
    public IEnumerable<IEnumerable<IComponent>> Scale(IEnumerable<IEnumerable<IComponentSource>> componentSources)
    {
        EnumeratorNode<IComponentSource>[] nodes = componentSources
            .Select(x => new EnumeratorNode<IComponentSource>(x.GetEnumerator()))
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
            foreach (EnumeratorNode<IComponentSource> node in nodes)
            {
                node.Dispose();
            }
        }
    }

    private static void MoveEnumeratorNodesEdgeEnumerators(IEnumerable<EnumeratorNode<IComponentSource>> nodes)
    {
        foreach (EnumeratorNode<IComponentSource> node in nodes.Where(x => x.HasValues))
        {
            node.MoveNext();
        }
    }

    protected abstract void ValidateCustomization(IComponent component, IComponent customizedComponent);

    protected abstract Axis GetAxis();
    
    protected abstract IComponent MergeComponentSources(IEnumerable<IComponentSource> componentSources);
    
    protected abstract int SelectDimension(IComponent component);

    private static void IterateEnumeratorNodeInDepth(IList<EnumeratorNode<IComponentSource>> nodes)
    {
        for (var i = 0; i < nodes.Count; i++)
        {
            if (!nodes[i].HasValues)
                continue;

            while (nodes[i].Value is not IComponent)
            {
                EnumeratorNode<IComponentSource> node = nodes[i];
                IEnumerator<IComponentSource> enumerator = node.Value.GetEnumerator();
                nodes[i] = new EnumeratorNode<IComponentSource>(enumerator, node);
                nodes[i].MoveNext();
            }
        }
    }

    private void ScaleEnumeratorNodesEdgeComponents(EnumeratorNode<IComponentSource>[] nodes)
    {
        IEnumerable<int> dimensions = nodes
            .Where(x => x.HasValues)
            .Select(x => x.Value)
            .OfType<IComponent>()
            .Select(SelectDimension);

        var dimension = LcmCounter.Count(dimensions);

        foreach (EnumeratorNode<IComponentSource> enumerator in nodes.Where(x => x.HasValues))
        {
            if (enumerator.Value is not IComponent component)
                throw new InvalidOperationException("Max depth component source must be a component");

            var factor = dimension / component.Size.Width;
            var value = component;

            if (factor is not 1)
            {
                value = (IComponent)component.ScaledBy(factor, GetAxis());
            }

            enumerator.Values.Add(value);
        }
    }

    private void FlushDepletedEnumeratorNodes(IList<EnumeratorNode<IComponentSource>> nodes)
    {
        for (var i = 0; i < nodes.Count; i++)
        {
            while (!nodes[i].HasValues && nodes[i].Next is not null)
            {
                EnumeratorNode<IComponentSource> node = nodes[i];
                node.Dispose();

                if (node.Next?.Value is ICustomizerComponentSource customizerComponentSource)
                {
                    var component = node.Values.Count switch
                    {
                        0 => Empty(),
                        1 => (IComponent)node.Values[0],
                        _ => MergeComponentSources(node.Values),
                    };

                    var customizedComponentSource = customizerComponentSource.Customize(component);

                    if (customizedComponentSource is not IComponent customizedComponent)
                        throw new InvalidOperationException("Max depth component source must be a component");

                    ValidateCustomization(component, customizedComponent);

                    nodes[i].Next!.Values.Add(customizedComponentSource);
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