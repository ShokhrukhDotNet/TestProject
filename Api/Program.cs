using Api.Configuration;
using Api.Middlewares;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllerlarni ishlashi uchun kerak
builder.Services.AddControllers();

// Api dokumentatsiyasi uchun kerak
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Loyihaga oid servislarni ulash
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddProjectServices(builder.Configuration);

var app = builder.Build();
// Ma'lumotlar bazasini migratsiya qilish
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    //await dataContext.Database.EnsureDeletedAsync();
    await dataContext.Database.MigrateAsync();
}
// Exception handler middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();

// Swagger faqat Developmentda ochiladi
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseDefaultFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Controllerlarni ulash
app.MapControllers();   // <<<--- MUHIM

app.Run();