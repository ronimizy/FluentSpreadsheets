using FluentSpreadsheets.Styles.Text;

namespace FluentSpreadsheets.GoogleSheets.Extensions;

public static class TextWrappingExtensions
{
    public static string? ToGoogleWrappingStrategy(this TextWrapping? wrapping)
    {
        return wrapping switch
        {
            null => null,
            TextWrapping.Overflow => "OVERFLOW_CELL",
            TextWrapping.Wrap => "WRAP",
            TextWrapping.Clip => "CLIP",
            _ => throw new ArgumentOutOfRangeException(nameof(wrapping), wrapping, null),
        };
    }
}