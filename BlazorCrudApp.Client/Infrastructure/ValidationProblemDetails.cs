namespace BlazorCrudApp.Client.Infrastructure;

public sealed record ValidationProblemDetails
{
    public string Title { get; init; } = string.Empty;
    public Dictionary<string, string[]> Errors { get; init; } = [];
}