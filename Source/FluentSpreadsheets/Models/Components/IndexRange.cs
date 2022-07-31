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
        // TODO: Validation

        Start = start;
        End = end;
    }

    public Index Start { get; }

    public Index End { get; }

    public override string ToString()
        => $"{Start} : {End}";
}