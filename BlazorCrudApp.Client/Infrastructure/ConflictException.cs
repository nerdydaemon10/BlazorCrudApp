namespace BlazorCrudApp.Client.Infrastructure;

public sealed class ConflictException(
    string message, 
    IReadOnlyDictionary<string, string[]> errors) : Exception(message)
{
    public IReadOnlyDictionary<string, string[]> Errors { get; } = errors;
}