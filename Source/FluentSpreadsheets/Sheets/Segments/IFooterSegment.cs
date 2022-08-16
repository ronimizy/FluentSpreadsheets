namespace FluentSpreadsheets.Sheets.Segments;

public interface IFooterSegment<in TData> : ISheetSegment
{
    IComponent BuildFooter(TData data);
}