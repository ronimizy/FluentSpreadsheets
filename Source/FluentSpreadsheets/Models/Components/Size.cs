namespace FluentSpreadsheets;

public record struct Size(int Width, int Height)
{
    public static Size operator*(Size left, Scale right)
        => new Size(left.Width * right.Horizontal, left.Height * right.Vertical);
}