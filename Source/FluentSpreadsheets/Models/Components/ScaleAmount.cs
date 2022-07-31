namespace FluentSpreadsheets;

public readonly struct ScaleAmount
{
    private readonly int _value;

    public ScaleAmount()
    {
        _value = 1;
    }

    public ScaleAmount(int value)
    {
        // TODO: Validation
        _value = value;
    }

    public static implicit operator int(ScaleAmount amount)
        => amount._value;

    public static implicit operator ScaleAmount(int value)
        => new ScaleAmount(value);

    public override string ToString()
        => _value.ToString();
}