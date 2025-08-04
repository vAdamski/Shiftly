namespace Shiftly.Application.Helpers;

public static class EnvironmentExtension
{
    public static bool IsDevelopment()
    {
        return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
    }
}