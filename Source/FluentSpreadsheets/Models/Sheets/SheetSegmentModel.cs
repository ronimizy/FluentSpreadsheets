namespace FluentSpreadsheets;

internal class SheetSegmentModel
{
    public SheetSegmentModel(IComponent? headerComponent, IList<IComponent> rowComponents, IComponent? footerComponent)
    {
        if (rowComponents is null)
            throw new ArgumentNullException(nameof(rowComponents));

        HeaderComponent = headerComponent;
        RowComponents = rowComponents;
        FooterComponent = footerComponent;
    }

    public IComponent? HeaderComponent { get; set; }

    public IList<IComponent> RowComponents { get; set; }

    public IComponent? FooterComponent { get; set; }
}