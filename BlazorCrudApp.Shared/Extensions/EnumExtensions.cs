using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BlazorCrudApp.Shared.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        var member = value.GetType().GetMember(value.ToString()).First();
        return member.GetCustomAttribute<DisplayAttribute>()?.Name
               ?? value.ToString();
    }
    public static int GetValue(this Enum value)
        => Convert.ToInt32(value);
}