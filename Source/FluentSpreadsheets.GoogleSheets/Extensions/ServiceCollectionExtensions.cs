using FluentSpreadsheets.Configuration;
using FluentSpreadsheets.GoogleSheets.Batching;
using FluentSpreadsheets.GoogleSheets.Batching.Implementations;
using FluentSpreadsheets.GoogleSheets.Configuration;
using FluentSpreadsheets.GoogleSheets.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpreadsheets.GoogleSheets.Extensions;

public static class ServiceCollectionExtensions
{
    public static IFluentSpreadsheetsConfigurationBuilder AddGoogleSheets(
        this IFluentSpreadsheetsConfigurationBuilder builder,
        Action<IGoogleBuilder>? configuration = null)
    {
        builder.Services.AddScoped<ISheetsServiceExecutor, InstantSheetsServiceExecutor>();
        builder.Services.AddScoped<IGoogleSheetsComponentRenderer, GoogleSheetComponentRenderer>();
        
        var googleBuilder = new GoogleBuilder(builder.Services);
        configuration?.Invoke(googleBuilder);

        return builder;
    }
}