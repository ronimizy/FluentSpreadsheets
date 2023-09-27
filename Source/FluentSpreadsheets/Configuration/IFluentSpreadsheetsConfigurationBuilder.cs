using Microsoft.Extensions.DependencyInjection;

namespace FluentSpreadsheets.Configuration;

public interface IFluentSpreadsheetsConfigurationBuilder
{
    IServiceCollection Services { get; }
}