namespace FluentSpreadsheets.Styles;

public readonly record struct Alignment(VerticalAlignment? Vertical, HorizontalAlignment? Horizontal)
{
    public Alignment Apply(Alignment alignment)
    {
        return new Alignment
        (
            alignment.Vertical ?? Vertical,
            alignment.Horizontal ?? Horizontal
        );
    }

    public Style AsStyle()
    {
        return new Style
        {
            Alignment = this,
        };
    }
}