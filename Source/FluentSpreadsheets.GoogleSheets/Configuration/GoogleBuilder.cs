using FluentSpreadsheets.GoogleSheets.Batching;
using FluentSpreadsheets.GoogleSheets.Batching.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentSpreadsheets.GoogleSheets.Configuration;

internal class GoogleBuilder : IGoogleBuilder
{
    private readonly IServiceCollection _collection;

    public GoogleBuilder(IServiceCollection collection)
    {
        _collection = collection;
    }

    public IGoogleBuilder UseBatching()
    {
        _collection.AddScoped<SheetsServiceBatchManager>();
        _collection.AddScoped<ISheetsServiceBatchProvider>(p => p.GetRequiredService<SheetsServiceBatchManager>());
        _collection.AddScoped<ISheetsServiceBatchScopeFactory>(p => p.GetRequiredService<SheetsServiceBatchManager>());

        _collection.RemoveAll(typeof(ISheetsServiceExecutor));
        _collection.AddScoped<ISheetsServiceExecutor, BatchingSheetsServiceExecutor>();

        return this;
    }
}