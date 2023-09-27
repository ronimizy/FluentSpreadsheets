using FluentSpreadsheets.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpreadsheets;

public static class ServiceCollectionExtensions
{
    public static IFluentSpreadsheetsConfigurationBuilder AddFluentSpreadsheets(this IServiceCollection collection)
    {
        return new FluentSpreadsheetsConfigurationBuilder(collection);
    }
}