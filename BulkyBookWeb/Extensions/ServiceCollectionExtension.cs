using OpenTelemetry;
using OpenTelemetry.Metrics;
using System.Diagnostics.Metrics;

namespace BulkyBookWeb.Extensions
{
    public static class ServiceCollectionExtension
    {
        static Meter s_meter = new Meter("BulkyBook", "1.0.0");
        static Counter<int> s_hatsSold = s_meter.CreateCounter<int>(name: "Vists",
                                                                    unit: "Request",
                                                                    description: "The number of visits to our book store");
        public static void ConfigureAddServicesExtension(this IServiceCollection services)
        {
            

        }
    }
}
