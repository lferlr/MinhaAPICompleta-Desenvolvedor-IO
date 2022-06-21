using DevIO.Api.Configuration;
using DevIO.Api.Extensions;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ConfigureServices

builder.Services.AddDbContext<MeuDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.WebApiConfig();
builder.Services.AddLoggingConfiguration();
builder.Services.ResolveDependencies();

// Configure

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors("Production");
    app.UseHsts();
}

app.UseAuthorization();
app.UseAuthentication();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.UseMvcConfiguration();
app.UseLoggingConfiguration();
app.Run();