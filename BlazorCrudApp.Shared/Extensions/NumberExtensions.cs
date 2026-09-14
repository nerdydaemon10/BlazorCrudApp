namespace BlazorCrudApp.Shared.Extensions;

public static class NumberExtensions
{
    public static string ToQuantityString(this int value, string singular, string plural)
    {
        return $"{value} {(value > 1 ? plural : singular)}";
    }
}