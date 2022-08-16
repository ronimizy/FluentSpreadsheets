namespace FluentSpreadsheets.Sheets.Segments;

public interface IRowSegment<in TData> : ISheetSegment
{
    IComponent BuildRow(TData data, int rowIndex);
}