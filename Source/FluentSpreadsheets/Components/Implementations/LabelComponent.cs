using FluentSpreadsheets.Visitors;

namespace FluentSpreadsheets.Implementations;

internal sealed class LabelComponent : ComponentBase, ILabelComponent
{
    public LabelComponent(string text, bool hasFormula)
    {
        Text = text;
        HasFormula = hasFormula;
    }

    public override Size Size => new Size(1, 1);

    public string Text { get; }
    public bool HasFormula { get; }

    public override void Accept(IComponentVisitor visitor)
        => visitor.Visit(this);

    public override string ToString()
        => Text;
}