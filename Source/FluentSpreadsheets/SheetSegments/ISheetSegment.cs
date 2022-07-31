namespace FluentSpreadsheets.SheetSegments;

public interface ISheetSegment<THeaderData, TRowData, TFooterData>
{
    IEnumerable<IComponent> BuildHeaders(THeaderData data);

    IEnumerable<IComponent> BuildRows(HeaderRowData<THeaderData, TRowData> data, int rowIndex);
    
    IEnumerable<IComponent> BuildFooters(HeaderFooterData<THeaderData, TFooterData> data);
}