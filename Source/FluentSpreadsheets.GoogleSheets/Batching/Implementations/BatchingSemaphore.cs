using FluentSpreadsheets.GoogleSheets.Configuration;
using Microsoft.Extensions.Options;

namespace FluentSpreadsheets.GoogleSheets.Batching.Implementations;

internal class BatchingSemaphore : IBatchingSemaphore
{
    private readonly SemaphoreSlim? _semaphore;

    public BatchingSemaphore(IOptions<GoogleSheetsBatchingConfiguration> options)
    {
        var value = options.Value;

        if (value.SimultaneousRequestCount < 1)
            return;

        _semaphore = new SemaphoreSlim(
            value.SimultaneousRequestCount,
            value.SimultaneousRequestCount);
    }

    public Task WaitAsync(CancellationToken cancellationToken)
    {
        return _semaphore?.WaitAsync(cancellationToken) ?? Task.CompletedTask;
    }

    public void Release()
    {
        _semaphore?.Release();
    }
}