using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class LabelComponent : ILabelComponent
{
    public LabelComponent(string text)
    {
        Text = text;
    }

    public Size Size => new Size(1, 1);

    public string Text { get; }

    public void Accept(IComponentVisitor visitor)
        => visitor.Visit(this);

    public override string ToString()
        => Text;
}