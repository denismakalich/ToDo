namespace ToDo.WebAPI.Extensions;

public static class AppExtensions
{
    public static void UseAuthorizationAndAuthentication(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}