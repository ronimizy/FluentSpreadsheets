namespace FluentSpreadsheets;

public readonly record struct Index(int Row, int Column)
{
    public override string ToString()
        => $"({Row}, {Column})";
}