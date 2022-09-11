namespace FluentSpreadsheets.Styles;

public interface IApplicable<T>
{
    T Apply(T style);
}