using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Register infrastructure and application services via extension methods
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular",
        policy =>
        {
            policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

// Swagger רק בסביבת Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global exception handling middleware - must be before controllers
app.UseMiddleware<BeyadAmi.Server.Api.Middleware.ExceptionMiddleware>();

app.MapControllers();

app.UseCors("Angular");
//app.UseAuthorization();

app.Run();