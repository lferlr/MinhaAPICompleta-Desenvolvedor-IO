using Elmah.Io.Extensions.Logging;

namespace DevIO.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services)
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

            return services;
        }
        
        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app) 
        {
            app.UseElmahIo();

            return app; 
        }
    }
}
