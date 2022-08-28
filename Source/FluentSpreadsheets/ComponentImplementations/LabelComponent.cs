using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.ComponentImplementations;

internal class LabelComponent : ComponentBase, ILabelComponent
{
    public LabelComponent(string text)
    {
        Text = text;
    }

    public override Size Size => new Size(1, 1);

    public string Text { get; }

    public override void Accept(IComponentVisitor visitor)
        => visitor.Visit(this);

    public override string ToString()
        => Text;
}