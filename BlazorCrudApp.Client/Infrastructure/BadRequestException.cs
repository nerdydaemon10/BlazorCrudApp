namespace BlazorCrudApp.Client.Infrastructure;

public sealed class BadRequestException(
    string message, 
    IReadOnlyDictionary<string, string[]> errors) : Exception(message)
{
    public IReadOnlyDictionary<string, string[]> Errors { get; } = errors;
}