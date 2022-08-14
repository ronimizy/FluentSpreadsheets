using FluentSpreadsheets.ComponentImplementations;
using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Sheets.Visitors;
using FluentSpreadsheets.Tools;

namespace FluentSpreadsheets.Sheets;

internal class SheetSegmentVisitor<THeaderData, TRowData, TFooterData> :
    IPrototypeHeaderSegmentVisitor<THeaderData>
{
    private readonly THeaderData _headerData;
    private readonly IReadOnlyCollection<TRowData> _rowData;
    private readonly TFooterData _footerData;
    private SheetSegmentModel? _segment;

    public SheetSegmentVisitor(
        THeaderData headerData,
        IReadOnlyCollection<TRowData> rowData,
        TFooterData footerData)
    {
        _headerData = headerData;
        _rowData = rowData;
        _footerData = footerData;
    }

    public SheetSegmentModel BuildSheetSegment(ISheetSegment builder)
    {
        builder.Accept(this);

        _segment ??= Visit(builder);

        if (builder is IPrototypeSegmentHeaderCustomizer<THeaderData> headerCustomizer && _segment.HeaderComponent is not null)
        {
            var width = _segment.HeaderComponent.Size.Width;
            var customizedHeader = headerCustomizer.CustomizeHeader(_segment.HeaderComponent, _headerData);

            if (!customizedHeader.Size.Width.Equals(width))
            {
                const string message = "The header customizer must not change the width of the header component.";
                throw new InvalidStructureException(message);
            }
            
            _segment.HeaderComponent = customizedHeader;
        }

        var segment = _segment;
        _segment = null;

        return segment;
    }

    public void Visit<TDestination>(IPrototypeHeaderSegment<THeaderData, TDestination> builder)
    {
        TDestination[] data = builder.Select(_headerData).ToArray();

        var headerVisitor = new PrototypeHeaderSegmentVisitor<TDestination>(data);
        var rowVisitor = new PrototypeRowSegmentVisitor<TDestination, TRowData>(data, _rowData);
        var footerVisitor = new PrototypeFooterSegmentVisitor<TDestination, TFooterData>(data, _footerData);

        builder.Accept(headerVisitor);
        builder.Accept(rowVisitor);
        builder.Accept(footerVisitor);

        IList<IComponent> headerComponents = headerVisitor.Components;
        IList<IList<IComponent>> rowComponents = rowVisitor.Components;
        IList<IComponent> footerComponents = footerVisitor.Components;

        ScalePrototypedComponents(data, headerComponents, rowComponents, footerComponents);

        IComponent? header = null;
        IList<IComponent> rows = Array.Empty<IComponent>();
        IComponent? footer = null;

        if (headerComponents.Count is not 0)
            header = new HStackComponent(headerComponents);

        if (rowComponents.Count is not 0)
        {
            var convertedRows = TransposeRows(rowComponents, _rowData.Count);

            rows = convertedRows
                .Select(x => (IComponent)new HStackComponent(x))
                .ToArray();
        }

        if (footerComponents.Count is not 0)
            footer = new HStackComponent(footerComponents);

        _segment = new SheetSegmentModel(header, rows, footer);
    }

    private SheetSegmentModel Visit(ISheetSegment builder)
    {
        var headerVisitor = new HeaderSegmentVisitor<THeaderData>(_headerData);
        var rowVisitor = new RowSegmentVisitor<THeaderData, TRowData>(_headerData, _rowData);
        var footerVisitor = new FooterSegmentVisitor<THeaderData, TFooterData>(_headerData, _footerData);

        builder.Accept(headerVisitor);
        builder.Accept(rowVisitor);
        builder.Accept(footerVisitor);

        var header = headerVisitor.Component;
        var row = rowVisitor.Components;
        var footer = footerVisitor.Component;

        return new SheetSegmentModel(header, row, footer);
    }

    private static void ScalePrototypedComponents<TDestination>(
        IReadOnlyCollection<TDestination> data,
        IList<IComponent> headerComponents,
        IList<IList<IComponent>> rowComponents,
        IList<IComponent> footerComponents)
    {
        for (var i = 0; i < data.Count; i++)
        {
            var rowWidth = CountRowWidth(i, headerComponents, rowComponents, footerComponents);

            if (headerComponents.Count is not 0)
                headerComponents[i] = ScaledComponent(headerComponents[i], rowWidth);

            if (rowComponents.Count is not 0)
            {
                IList<IComponent>? segment = rowComponents[i];

                for (var j = 0; j < segment.Count; j++)
                {
                    segment[j] = ScaledComponent(segment[j], rowWidth);
                }
            }

            if (footerComponents.Count is not 0)
                footerComponents[i] = ScaledComponent(footerComponents[i], rowWidth);
        }
    }

    private static IComponent ScaledComponent(IComponent component, int width)
    {
        var scaleFactor = width / component.Size.Width;
        return scaleFactor is 1 ? component : component.ScaledBy(scaleFactor, Axis.Horizontal);
    }

    private static int CountRowWidth(
        int i,
        IList<IComponent> headers,
        IList<IList<IComponent>> rows,
        IList<IComponent> footers)
    {
        IEnumerable<int> rowWidthEnumerable = Enumerable.Empty<int>();

        if (headers.Count is not 0)
            rowWidthEnumerable = rowWidthEnumerable.Append(headers[i].Size.Width);

        if (rows.Count is not 0)
            rowWidthEnumerable = rowWidthEnumerable.Concat(rows[i].Select(x => x.Size.Width));

        if (footers.Count is not 0)
            rowWidthEnumerable = rowWidthEnumerable.Append(footers[i].Size.Width);

        var rowWidth = LcmCounter.Count(rowWidthEnumerable);
        return rowWidth;
    }

    private static IEnumerable<IEnumerable<IComponent>> TransposeRows(
        IList<IList<IComponent>> rows,
        int length)
    {
        for (var i = 0; i < length; i++)
        {
            var localI = i;
            yield return rows.Select(x => x[localI]);
        }
    }
}