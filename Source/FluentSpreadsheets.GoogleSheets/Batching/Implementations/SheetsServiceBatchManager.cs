using Google.Apis.Sheets.v4;

namespace FluentSpreadsheets.GoogleSheets.Batching.Implementations;

internal class SheetsServiceBatchManager : ISheetsServiceBatchScopeFactory, ISheetsServiceBatchProvider
{
    private readonly SheetsService _service;
    private readonly IBatchingSemaphore _semaphore;

    private SheetsServiceBatch? _batch;
    private int _usageCount;

    public SheetsServiceBatchManager(SheetsService service, IBatchingSemaphore semaphore)
    {
        _service = service;
        _semaphore = semaphore;
    }

    public ISheetsServiceBatchScope CreateScope()
    {
        if (_batch is not null)
        {
            _usageCount++;
            return _batch;
        }

        _usageCount = 1;
        _batch = new SheetsServiceBatch(_service, OnDisposed, _semaphore);

        return _batch;
    }

    public ISheetsServiceBatch? TryGetBatch()
    {
        return _batch;
    }

    private void OnDisposed()
    {
        _usageCount--;

        if (_usageCount is 0)
            _batch = null;
    }
}