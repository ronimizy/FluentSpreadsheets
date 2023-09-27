using FluentSpreadsheets.ClosedXML.Rendering;
using FluentSpreadsheets.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpreadsheets.ClosedXML.Extensions;

public static class ServiceCollectionExtensions
{
    public static IFluentSpreadsheetsConfigurationBuilder AddClosedXml(
        this IFluentSpreadsheetsConfigurationBuilder builder)
    {
        builder.Services.AddScoped<IClosedXmlComponentRenderer, ClosedXmlComponentRenderer>();
        return builder;
    }
}