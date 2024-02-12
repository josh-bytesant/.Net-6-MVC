using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Extentions
{
    public static class ServiceExtention
    {
        public static void ConfigureServices(this IServiceCollection services, ConfigurationManager configuration) {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }
    }
}
