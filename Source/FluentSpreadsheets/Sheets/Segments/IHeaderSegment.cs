namespace FluentSpreadsheets.Sheets.Segments;

public interface IHeaderSegment<in TData> : ISheetSegment
{
    IComponent BuildHeader(TData data);
}