namespace FluentSpreadsheets.Sheets.Segments;

public interface IPrototypeHeaderSegment<in TSource, out TDestination> : IPrototypeSheetSegment
{
    IEnumerable<TDestination> SelectHeaderData(TSource source);
}