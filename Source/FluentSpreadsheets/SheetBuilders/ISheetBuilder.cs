using FluentSpreadsheets.SheetSegments;

namespace FluentSpreadsheets.SheetBuilders;

public interface ISheetBuilder
{
    IComponent Build<THeaderData, TRowData, TFooterData>(
        IReadOnlyCollection<ISheetSegment<THeaderData, TRowData, TFooterData>> segments,
        SheetData<THeaderData, TRowData, TFooterData> data);
}