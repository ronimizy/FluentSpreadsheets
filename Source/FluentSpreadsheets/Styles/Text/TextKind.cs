namespace FluentSpreadsheets.Styles.Text;

[Flags]
public enum TextKind
{
    Common = 1 << 0,
    Bold = 1 << 1,
    Italic = 1 << 2,
}