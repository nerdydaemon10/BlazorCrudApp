namespace BlazorCrudApp.Client.Infrastructure;

public sealed class ServerUnavailableException(
    string message, Exception innerException) : Exception(message, innerException);