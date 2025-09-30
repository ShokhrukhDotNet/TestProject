using Api.Configuration;
using Api.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TestProject API",
        Version = "v1"
    });
});

builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddProjectServices(builder.Configuration);
var app = builder.Build();
await app.UseMigrationAsync();
app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestProject API v1");
    });
}

app.UseStaticFiles();
app.UseDefaultFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
