using Api.Configuration;
using Api.Middlewares;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Controllerlarni ulash
builder.Services.AddControllers();

// Swagger (OpenAPI) ulash
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TestProject API",
        Version = "v1"
    });
});

// Loyihaga oid servislarni ulash
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddProjectServices(builder.Configuration);

var app = builder.Build();

// Database migratsiya qilish
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    await dataContext.Database.MigrateAsync();
}

// Global exception handler middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();

// Swagger Development va Production muhitida ochiladi
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestProject API v1");
        c.RoutePrefix = string.Empty; // Swagger ochilganda bevosita root URL’dan ochiladi
    });
}

app.UseStaticFiles();
app.UseDefaultFiles();
// app.UseHttpsRedirection(); // Docker ichida kerak bo‘lmasa qoldirib ketishingiz mumkin

app.UseAuthentication();
app.UseAuthorization();

// Controllerlarni ulash
app.MapControllers();

app.Run();
