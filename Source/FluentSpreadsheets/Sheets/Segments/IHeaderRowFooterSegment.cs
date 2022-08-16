namespace FluentSpreadsheets.Sheets.Segments;

public interface IHeaderRowFooterSegment<in THeaderData, in TRowData, in TFooterData> :
    IHeaderSegment<THeaderData>,
    IRowSegment<TRowData>,
    IFooterSegment<TFooterData> { }