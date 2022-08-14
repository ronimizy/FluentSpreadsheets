using FluentSpreadsheets.Sheets.Segments;
using FluentSpreadsheets.Sheets.Visitors;

namespace FluentSpreadsheets.Sheets;

public abstract class PrototypeHeaderRowFooterSegmentBase<THeaderSource, THeaderDestination, TRowData, TFooterData> :
    HeaderRowFooterSegmentBase<THeaderDestination, TRowData, TFooterData>,
    IPrototypeHeaderSegment<THeaderSource, THeaderDestination>
{
    public abstract IEnumerable<THeaderDestination> Select(THeaderSource source);

    public override void Accept(ISheetSegmentVisitor visitor)
    {
        if (visitor is IPrototypeHeaderSegmentVisitor<THeaderSource> prototypeHeaderSegmentVisitor)
            prototypeHeaderSegmentVisitor.Visit(this);
        
        base.Accept(visitor);
    }
}