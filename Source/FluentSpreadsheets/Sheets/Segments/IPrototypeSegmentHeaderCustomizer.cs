namespace FluentSpreadsheets.Sheets.Segments;

public interface IPrototypeSegmentHeaderCustomizer<in TData> : ISheetSegment
{
    IComponent CustomizeHeader(IComponent component, TData data);
}