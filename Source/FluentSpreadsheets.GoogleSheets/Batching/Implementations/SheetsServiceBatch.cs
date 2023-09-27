using FluentSpreadsheets.GoogleSheets.Models;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace FluentSpreadsheets.GoogleSheets.Batching.Implementations;

internal class SheetsServiceBatch : ISheetsServiceBatch, ISheetsServiceBatchScope
{
    private readonly List<ValueUpdateCommand> _valueCommands;
    private readonly List<SpreadsheetUpdateCommand> _spreadsheetCommands;

    private readonly SheetsService _service;
    private readonly Action _onDisposed;
    private readonly IBatchingSemaphore _semaphore;

    public SheetsServiceBatch(SheetsService service, Action onDisposed, IBatchingSemaphore semaphore)
    {
        _service = service;
        _onDisposed = onDisposed;
        _semaphore = semaphore;

        _valueCommands = new List<ValueUpdateCommand>();
        _spreadsheetCommands = new List<SpreadsheetUpdateCommand>();
    }

    public void EnqueueValueUpdates(
        string spreadsheetId,
        IEnumerable<ValueRange> ranges,
        CancellationToken cancellationToken)
    {
        var command = new ValueUpdateCommand(spreadsheetId, ranges, cancellationToken);
        _valueCommands.Add(command);
    }

    public void EnqueueSpreadsheetUpdates(
        string spreadsheetId,
        IEnumerable<Request> requests,
        CancellationToken cancellationToken)
    {
        var command = new SpreadsheetUpdateCommand(spreadsheetId, requests, cancellationToken);
        _spreadsheetCommands.Add(command);
    }

    public async ValueTask DisposeAsync()
    {
        _onDisposed.Invoke();

        IEnumerable<Task> valueTasks = _valueCommands
            .Where(x => x.CancellationToken.IsCancellationRequested is false)
            .GroupBy(x => x.SpreadsheetId)
            .Select(async grouping =>
            {
                var request = new BatchUpdateValuesRequest
                {
                    Data = grouping.SelectMany(x => x.Ranges).ToArray(),
                    ValueInputOption = ValueInputOption.UserEntered,
                };

                await _semaphore.WaitAsync(default);


                try
                {
                    var batchRequest = _service.Spreadsheets.Values.BatchUpdate(request, grouping.Key);
                    await batchRequest.ExecuteAsync();
                }
                finally
                {
                    _semaphore.Release();
                }
            });

        IEnumerable<Task> spreadsheetTasks = _spreadsheetCommands
            .Where(x => x.CancellationToken.IsCancellationRequested is false)
            .GroupBy(x => x.SpreadsheetId)
            .Select(async grouping =>
            {
                var request = new BatchUpdateSpreadsheetRequest
                {
                    Requests = grouping.SelectMany(x => x.Requests).ToArray(),
                };

                await _semaphore.WaitAsync(default);

                try
                {
                    var batchRequest = _service.Spreadsheets.BatchUpdate(request, grouping.Key);
                    await batchRequest.ExecuteAsync();
                }
                finally
                {
                    _semaphore.Release();
                }
            });


        IEnumerable<Task> tasks = valueTasks.Concat(spreadsheetTasks);

        await Task.WhenAll(tasks);
    }

    private record ValueUpdateCommand(
        string SpreadsheetId,
        IEnumerable<ValueRange> Ranges,
        CancellationToken CancellationToken);

    private record SpreadsheetUpdateCommand(
        string SpreadsheetId,
        IEnumerable<Request> Requests,
        CancellationToken CancellationToken);
}