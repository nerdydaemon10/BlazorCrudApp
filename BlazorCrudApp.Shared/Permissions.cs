namespace BlazorCrudApp.Shared;

public static class Permissions
{
    public static class Products
    {
        public const string Create = "products:create";
        public const string Read = "products:read";
        public const string Edit = "products:edit";
        public const string Delete = "products:delete";
    }
}