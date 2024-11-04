using Microsoft.Extensions.Configuration;

namespace FeiraTech.Infrastructure.Extensions
{
    public static class ExtensionsDatabase
    {
        public static string GetConnectionStringData(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("Database")!;
        }
    }
}
