using FluentSpreadsheets.Styles;

namespace FluentSpreadsheets;

internal static class ApplicableExtensions
{
    public static T? TryApply<T>(this T? value, T? applied) where T : struct, IApplicable<T>
        => applied is null ? value : value?.Apply(applied.Value) ?? applied;
}