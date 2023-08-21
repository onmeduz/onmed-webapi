using Serilog;

namespace OnMed.WebApi.Configurations;

public static class LoggerConfiguration
{
    public static void ConfigureLogger(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
    }
}
