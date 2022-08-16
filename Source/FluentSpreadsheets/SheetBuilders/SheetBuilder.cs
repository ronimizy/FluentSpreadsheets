using FluentSpreadsheets.ComponentImplementations;
using FluentSpreadsheets.Sheets;
using FluentSpreadsheets.Sheets.Segments;

namespace FluentSpreadsheets.SheetBuilders;

public class SheetBuilder : ISheetBuilder
{
    public IComponent Build<TRowData>(
        IReadOnlyCollection<ISheetSegment> builders,
        IReadOnlyCollection<TRowData> rowData)
    {
        var visitor = new SheetSegmentVisitor<Unit, TRowData, Unit>(Unit.Value, rowData, Unit.Value);

        SheetSegmentModel[] segments = builders.Select(visitor.BuildSheetSegment).ToArray();

        HStackComponent[] rows = Enumerable.Range(0, rowData.Count)
            .Select(i => segments.Select(s => s.RowComponents[i]))
            .Select(x => new HStackComponent(x))
            .ToArray();

        var width = segments.Sum(x => x.RowComponents.Count is 0 ? 0 : x.RowComponents[0].Size.Width);

        return new VStackComponent(rows, width);
    }

    public IComponent Build<THeaderData, TRowData>(
        IReadOnlyCollection<ISheetSegment> builders,
        THeaderData headerData,
        IReadOnlyCollection<TRowData> rowData)
    {
        var visitor = new SheetSegmentVisitor<THeaderData, TRowData, Unit>(headerData, rowData, Unit.Value);

        SheetSegmentModel[] segments = builders.Select(visitor.BuildSheetSegment).ToArray();

        if (segments.Any(x => x.HeaderComponent is null))
            throw new InvalidStructureException("Header component cannot be null");

        IEnumerable<IComponent> headerComponents = segments.Select(x => x.HeaderComponent!);
        var header = new HStackComponent(headerComponents);

        IEnumerable<HStackComponent> rows = Enumerable.Range(0, rowData.Count)
            .Select(i => segments.Select(s => s.RowComponents[i]))
            .Select(x => new HStackComponent(x));

        HStackComponent[] components = Enumerable.Repeat(header, 1)
            .Concat(rows)
            .ToArray();

        return new VStackComponent(components, header.Size.Width);
    }

    public IComponent Build<THeaderData, TRowData, TFooterData>(
        IReadOnlyCollection<ISheetSegment> segments,
        THeaderData headerData,
        IReadOnlyCollection<TRowData> rowData,
        TFooterData footerData)
    {
        var visitor = new SheetSegmentVisitor<THeaderData, TRowData, TFooterData>(headerData, rowData, footerData);

        SheetSegmentModel[] sheetSegments = segments.Select(visitor.BuildSheetSegment).ToArray();

        if (sheetSegments.Any(x => x.HeaderComponent is null))
            throw new InvalidStructureException("Header component cannot be null");

        if (sheetSegments.Any(x => x.FooterComponent is null))
            throw new InvalidStructureException("Footer component cannot be null");

        IEnumerable<IComponent> headerComponents = sheetSegments.Select(x => x.HeaderComponent!);
        var header = new HStackComponent(headerComponents);

        IEnumerable<IComponent> footerComponents = sheetSegments.Select(x => x.FooterComponent!);
        var footer = new HStackComponent(footerComponents);

        IEnumerable<HStackComponent> rows = Enumerable.Range(0, rowData.Count)
            .Select(i => sheetSegments.Select(s => s.RowComponents[i]))
            .Select(x => new HStackComponent(x));

        HStackComponent[] components = Enumerable.Repeat(header, 1)
            .Concat(rows)
            .Append(footer)
            .ToArray();

        return new VStackComponent(components, header.Size.Width);
    }
}