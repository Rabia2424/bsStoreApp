using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;

namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            // DbContext'in bağlanacağı veritabanı bağlantı dizesini alın
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // DbContext'i servis olarak ekleyin
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
