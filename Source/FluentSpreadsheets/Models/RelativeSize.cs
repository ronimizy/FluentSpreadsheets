namespace FluentSpreadsheets;

public struct RelativeSize
{
    public RelativeSize(double value)
    {
        if (value < 0)
            throw new ValidationException($"Relative size value cannot be {value} (must be >= 0)");

        Value = value;
    }

    public double Value { get; }

    public static implicit operator RelativeSize(double value)
        => new RelativeSize(value);
}