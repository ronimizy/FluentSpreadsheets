using Google.Apis.Sheets.v4;

namespace FluentSpreadsheets.GoogleSheets.Batching.Implementations;

internal class SheetsServiceBatchManager : ISheetsServiceBatchScopeFactory, ISheetsServiceBatchProvider
{
    private readonly SheetsService _service;
    private SheetsServiceBatch? _batch;

    public SheetsServiceBatchManager(SheetsService service)
    {
        _service = service;
    }

    public ISheetsServiceBatchScope CreateScope()
    {
        return _batch ??= new SheetsServiceBatch(_service, () => _batch = null);
    }

    public ISheetsServiceBatch? TryGetBatch()
    {
        return _batch;
    }
}