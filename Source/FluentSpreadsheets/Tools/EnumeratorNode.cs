namespace FluentSpreadsheets.Tools;

internal class EnumeratorNode<T> : IDisposable
{
    private readonly IEnumerator<T> _enumerator;

    public EnumeratorNode(IEnumerator<T> enumerator, EnumeratorNode<T>? next = null)
    {
        _enumerator = enumerator;
        Next = next;

        Values = new List<T>();
        HasValues = true;
    }


    public EnumeratorNode<T>? Next { get; }

    public T Value => _enumerator.Current;

    public bool HasValues { get; private set; }

    public List<T> Values { get; }

    public void MoveNext()
    {
        HasValues = _enumerator.MoveNext();
    }

    public void Dispose()
        => _enumerator.Dispose();
}