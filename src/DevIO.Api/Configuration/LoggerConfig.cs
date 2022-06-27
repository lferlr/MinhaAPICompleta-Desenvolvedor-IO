using DevIO.Api.Extensions;
using Elmah.Io.Extensions.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace DevIO.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "0cf496d58c834be582d4708099ae5566";
                o.LogId = new Guid("80321166-d903-4c84-a41d-c03a4736c209");
            });

            //services.AddLogging(builder =>
            //{
            //    builder.AddElmahIo(o =>
            //    {
            //        o.ApiKey = "0cf496d58c834be582d4708099ae5566";
            //        o.LogId  = new Guid("80321166-d903-4c84-a41d-c03a4736c209");
            //    });
            //    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            //});

            services.AddHealthChecks()
                .AddElmahIoPublisher(options =>
                {
                    options.ApiKey = "0cf496d58c834be582d4708099ae5566";
                    options.LogId = new Guid("80321166-d903-4c84-a41d-c03a4736c209");
                    options.HeartbeatId = "API Fornecedores";

                })
                .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");
            
            services.AddHealthChecksUI()
                .AddInMemoryStorage();

            return services;
        }
        
        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app) 
        {
            app.UseElmahIo();

            app.UseHealthChecks("/api/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/api/hc-ui";
                options.ResourcesPath = "/api/hc-ui-resources";

                options.UseRelativeApiPath = false;
                options.UseRelativeResourcesPath = false;
                options.UseRelativeWebhookPath = false;
            });

            return app; 
        }
    }
}
