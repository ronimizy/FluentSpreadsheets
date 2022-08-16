using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Sheets.Visitors;

namespace FluentSpreadsheets.Sheets;

public abstract class PrototypeSegmentBase<THeaderSource, THeaderDestination, TRowData, TFooterData> :
    SegmentBase<THeaderDestination, TRowData, TFooterData>,
    IPrototypeHeaderSegment<THeaderSource, THeaderDestination>
{
    public abstract IEnumerable<THeaderDestination> SelectHeaderData(THeaderSource source);

    public override void Accept(ISheetSegmentVisitor visitor)
    {
        if (visitor is IPrototypeHeaderSegmentVisitor<THeaderSource> prototypeHeaderSegmentVisitor)
            prototypeHeaderSegmentVisitor.Visit(this);
        
        base.Accept(visitor);
    }
}