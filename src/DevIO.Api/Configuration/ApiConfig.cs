namespace DevIO.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                        builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());

                options.AddPolicy("Production",
                        builder => builder.WithMethods("GET")
                        .WithOrigins("http://desenvolvedor.io")
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader());
            });

            return services;
        }

        public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            return app;
        }
    }
}
