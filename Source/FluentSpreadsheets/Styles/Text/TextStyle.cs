using System.Drawing;

namespace FluentSpreadsheets.Styles.Text;

public readonly record struct TextStyle(Color? Color, TextKind? Kind) : IApplicable<TextStyle>
{
    public TextStyle Apply(TextStyle style)
    {
        return new TextStyle
        (
            style.Color ?? Color,
            style.Kind ?? Kind
        );
    }

    public Style AsStyle()
    {
        return new Style
        {
            Text = this,
        };
    }
}