namespace FluentSpreadsheets.Sheets.Segments;

public interface IHeaderRowSegment<in THeaderData, in TRowData> :
    IHeaderSegment<THeaderData>,
    IRowSegment<TRowData> { }