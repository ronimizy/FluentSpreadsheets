using FluentSpreadsheets.ComponentImplementations;
using FluentSpreadsheets.SheetSegments;
using FluentSpreadsheets.Tools;
using static FluentSpreadsheets.ComponentFactory;

namespace FluentSpreadsheets.SheetBuilders;

public class SheetBuilder : ISheetBuilder
{
    public IComponent Build<THeaderData, TRowData, TFooterData>(
        IReadOnlyCollection<ISheetSegment<THeaderData, TRowData, TFooterData>> segments,
        SheetData<THeaderData, TRowData, TFooterData> data)
    {
        IComponent[] headers = segments
            .SelectMany(x => x.BuildHeaders(data.HeaderData))
            .ToArray();

        var rows = new List<IComponent[]>(data.RowData.Count);
        var i = 0;
        var headerCount = headers.Length;

        foreach (var rowData in data.RowData)
        {
            var headerRowData = new HeaderRowData<THeaderData, TRowData>(data.HeaderData, rowData);

            IComponent[] row = segments.SelectMany(x => x.BuildRows(headerRowData, i)).ToArray();
            rows.Add(row);

            if (!rows[i].Length.Equals(headerCount))
            {
                // TODO: Proper exception
                throw new Exception();
            }

            i++;
        }

        var headerFooterData = new HeaderFooterData<THeaderData, TFooterData>(data.HeaderData, data.FooterData);

        IComponent[] footers = segments
            .SelectMany(x => x.BuildFooters(headerFooterData))
            .ToArray();

        if (!footers.Length.Equals(headerCount))
        {
            // TODO: Proper exception
            throw new Exception();
        }

        ScaleColumns(headerCount, rows, headers, footers);
        var headerHeight = ScaleHeaderRow(headers);
        
        
        var footerHeight = ScaleFooterRow(footers);

        var width = headers.Sum(x => x.Size.Width);
        IComponent[] rowHStacks = rows.Select(ScaleRow).ToArray();

        var rowStack = new VStackComponent(rowHStacks, width);
        var headerStack = new HStackComponent(headers, headerHeight);
        var footerStack = new HStackComponent(footers, footerHeight);

        var stacks = new IComponent[]
        {
            headerStack,
            rowStack,
            footerStack
        };

        return new VStackComponent(stacks, width);
    }

    private static void ScaleColumns(
        int columnCount,
        IList<IComponent[]> rows,
        IList<IComponent> headers,
        IList<IComponent> footers)
    {
        for (var j = 0; j < columnCount; j++)
        {
            var localJ = j;
            IEnumerable<int> widths = rows.Select(x => x[localJ].Size.Width)
                .Append(headers[j].Size.Width)
                .Append(footers[j].Size.Width)
                .Where(x => x is not 0);

            var width = LcmCounter.Count(widths);

            headers[j] = ScaleComponentWidth(headers[j], width);
            footers[j] = ScaleComponentWidth(footers[j], width);

            foreach (IComponent[] row in rows)
            {
                row[j] = ScaleComponentWidth(row[j], width);
            }
        }
    }

    private static int ScaleHeaderRow(IList<IComponent> headers)
    {
        IEnumerable<int> heights = headers.Select(x => x.Size.Height);
        var height = LcmCounter.Count(heights);

        for (var i = 0; i < headers.Count; i++)
        {
            headers[i] = ScaleComponentHeight(headers[i], height);
        }

        return height;
    }

    private static int ScaleFooterRow(IList<IComponent> footers)
    {
        IEnumerable<int> heights = footers.Select(x => x.Size.Height);
        var height = LcmCounter.Count(heights);

        for (var i = 0; i < footers.Count; i++)
        {
            footers[i] = ScaleComponentHeight(footers[i], height);
        }

        return height;
    }

    private static IComponent ScaleRow(IComponent[] row)
    {
        IEnumerable<int> heights = row.Select(x => x.Size.Height);
        var height = LcmCounter.Count(heights);

        for (var i = 0; i < row.Length; i++)
        {
            row[i] = ScaleComponentHeight(row[i], height);
        }

        return new HStackComponent(row, height);
    }

    private static IComponent ScaleComponentWidth(IComponent component, int width)
    {
        var componentWidth = component.Size.Width;

        if (componentWidth is 0)
            return Empty(component.Size with { Width = width });

        var scaleFactor = width / componentWidth;

        return scaleFactor is 1 ? component : component.ScaledBy(scaleFactor, Axis.Horizontal);
    }

    private static IComponent ScaleComponentHeight(IComponent component, int height)
    {
        var componentHeight = component.Size.Height;

        if (componentHeight is 0)
            return Empty(component.Size with { Height = height });

        var scaleFactor = height / componentHeight;

        return scaleFactor is 1 ? component : component.ScaledBy(scaleFactor, Axis.Vertical);
    }
}