using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Sheets.Visitors;

namespace FluentSpreadsheets.Sheets;

public abstract class PrototypeHeaderRowSegmentBase<TSourceHeaderData, TDestinationHeaderData, TRowData> :
    HeaderRowSegmentBase<TDestinationHeaderData, TRowData>,
    IPrototypeHeaderSegment<TSourceHeaderData, TDestinationHeaderData>
{
    public abstract IEnumerable<TDestinationHeaderData> Select(TSourceHeaderData source);

    public override void Accept(ISheetSegmentVisitor visitor)
    {
        if (visitor is IPrototypeHeaderSegmentVisitor<TSourceHeaderData> prototypeHeaderSegmentVisitor)
            prototypeHeaderSegmentVisitor.Visit(this);
        
        base.Accept(visitor);
    }
}