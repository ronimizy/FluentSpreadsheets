namespace FluentSpreadsheets.GoogleSheets.Batching;

internal interface IBatchingSemaphore
{
    Task WaitAsync(CancellationToken cancellationToken);

    void Release();
}