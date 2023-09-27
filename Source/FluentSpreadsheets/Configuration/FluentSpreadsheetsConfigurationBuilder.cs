using Microsoft.Extensions.DependencyInjection;

namespace FluentSpreadsheets.Configuration;

internal sealed class FluentSpreadsheetsConfigurationBuilder : IFluentSpreadsheetsConfigurationBuilder
{
    public FluentSpreadsheetsConfigurationBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; }
}