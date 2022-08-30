namespace FluentSpreadsheets;

public struct Unit
{
    public static Unit Value => new Unit();
    
    public static Task<Unit> Task { get; } = System.Threading.Tasks.Task.FromResult(Value);
}