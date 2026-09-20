namespace BlazorCrudApp.Api;

public sealed class ConflictException(string message, string? propertyName = null) : Exception(message)
{
    public string? PropertyName { get; } = propertyName;
}