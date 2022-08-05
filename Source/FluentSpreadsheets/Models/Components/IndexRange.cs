namespace FluentSpreadsheets;

/// <summary>
///     Index range represented with a start and end index.
///     Start – top left corner of the range (inclusive).
///     End – bottom right corner of the range (exclusive).
/// </summary>
public readonly struct IndexRange
{
    public IndexRange(Index start, Index end)
    {
        if (start.Row > end.Row || start.Column > end.Column)
        {
            const string message = "IndexRange start must precede end.";
            throw new ValidationException(message);
        }

        Start = start;
        End = end;
    }

    public Index Start { get; }

    public Index End { get; }

    public override string ToString()
        => $"{Start} : {End}";
}