using FluentSpreadsheets.Sheets;
using FluentSpreadsheets.Sheets.Segments;

namespace FluentSpreadsheets.SheetBuilders;

public interface ISheetBuilder
{
    IComponent Build<TRowData>(
        IReadOnlyCollection<ISheetSegment> builders,
        IReadOnlyCollection<TRowData> rowData);

    IComponent Build<THeaderData, TRowData>(
        IReadOnlyCollection<ISheetSegment> builders,
        THeaderData headerData,
        IReadOnlyCollection<TRowData> rowData);

    IComponent Build<THeaderData, TRowData, TFooterData>(
        IReadOnlyCollection<ISheetSegment> segments,
        THeaderData headerData,
        IReadOnlyCollection<TRowData> rowData,
        TFooterData footerData);
}