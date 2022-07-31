namespace FluentSpreadsheets;

public readonly record struct Scale(ScaleAmount Horizontal, ScaleAmount Vertical)
{
    public static readonly Scale None = new Scale(1, 1);

    public static Scale operator *(Scale left, Scale right)
        => new Scale(left.Horizontal * right.Horizontal, left.Vertical * right.Vertical);

    public bool IsNone => Equals(None);
}