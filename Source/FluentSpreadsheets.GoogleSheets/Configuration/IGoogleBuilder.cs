namespace FluentSpreadsheets.GoogleSheets.Configuration;

public interface IGoogleBuilder
{
    IGoogleBuilder UseBatching(Action<GoogleSheetsBatchingConfiguration>? configuration = null);
}