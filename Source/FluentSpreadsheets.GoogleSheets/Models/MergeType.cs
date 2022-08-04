namespace FluentSpreadsheets.GoogleSheets.Models;

public readonly struct MergeType
{
    private readonly string _value;

    private MergeType(string value)
    {
        _value = value;
    }

    public static MergeType All => new("MERGE_ALL");
    public static MergeType Columns => new("MERGE_COLUMNS");
    public static MergeType Rows => new("MERGE_ROWS");

    public static implicit operator string(MergeType type)
        => type.ToString();

    public override string ToString()
        => _value;
}