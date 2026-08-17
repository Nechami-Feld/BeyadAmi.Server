using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Services;
using BeyadAmi.Server.Application.Settings;
using BeyadAmi.Server.Infrastructure.Extensions;
using BeyadAmi.Server.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Register infrastructure and application services via extension methods
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddHttpClient<IAddressService, AddressService>();

// JWT configuration
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtOptions = jwtSection.Get<JwtOptions>();
if (jwtOptions != null)
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(GetJwtKeyBytes(jwtOptions.Key))
            };
        });

    builder.Services.AddAuthorization();
}

static byte[] GetJwtKeyBytes(string? key)
{
    if (string.IsNullOrWhiteSpace(key))
        throw new InvalidOperationException("JWT configuration error: 'Jwt:Key' is not set.");

    // Try to interpret the key as base64 first (recommended for binary secrets)
    try
    {
        var base64 = Convert.FromBase64String(key);
        if (base64.Length >= 32) // 256 bits == 32 bytes
            return base64;
        // if base64 decoded but too short, fall through to try UTF8
    }
    catch
    {
        // not base64, will treat as UTF8 string
    }

    var utf8 = Encoding.UTF8.GetBytes(key);
    if (utf8.Length < 32)
        throw new InvalidOperationException("JWT key size is too small. HS256 requires a key of at least 256 bits (32 bytes). Provide a longer secret or a base64-encoded 32+ byte key.");

    return utf8;
}

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer <token>'"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular",
        policy =>
        {
            policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


// Global exception handling middleware - must be before controllers
app.UseMiddleware<BeyadAmi.Server.Api.Middleware.ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.UseCors("Angular");
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();