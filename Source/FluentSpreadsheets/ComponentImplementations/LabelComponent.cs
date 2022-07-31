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

    public Task AcceptAsync(IComponentVisitor visitor, CancellationToken cancellationToken)
        => visitor.VisitAsync(this, cancellationToken);

    public override string ToString()
        => Text;
}