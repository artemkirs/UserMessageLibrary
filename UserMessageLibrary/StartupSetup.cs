using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserMessageLibrary.Data;

namespace UserMessageLibrary
{
    /// <summary>
    /// Стартовый файл, для получения подключения через основное приложение
    /// </summary>
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
             services.AddDbContext<UserMessageContext>(options =>
                 options.UseSqlServer(connectionString));
    }
}
