namespace FluentSpreadsheets.Sheets.Segments;

public interface IPrototypeHeaderSegment<in TSource, out TDestination> : IPrototypeSheetSegment
{
    IEnumerable<TDestination> Select(TSource source);
}