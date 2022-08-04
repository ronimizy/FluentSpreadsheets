namespace FluentSpreadsheets.GoogleSheets.Models;

public readonly struct Dimension
{
    private readonly string _value;

    private Dimension(string value)
    {
        _value = value;
    }

    public static Dimension Rows => new("ROWS");
    public static Dimension Columns => new("COLUMNS");

    public static implicit operator string(Dimension dimension)
        => dimension.ToString();

    public override string ToString()
        => _value;
}
