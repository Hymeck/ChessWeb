using System;

namespace ChessWeb.Application
{
    public static class BuildData
    {
        private static bool IsDevelopment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        
        public static string HostPort =>
            IsDevelopment
                ? "5000"
                : Environment.GetEnvironmentVariable("PORT");
    }
}