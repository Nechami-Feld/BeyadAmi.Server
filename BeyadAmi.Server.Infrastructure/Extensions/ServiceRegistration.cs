using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BeyadAmi.Server.Infrastructure.Persistence;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Infrastructure.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Services;

namespace BeyadAmi.Server.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IDeviceCategoryRepository, DeviceCategoryRepository>();

            // Validators
            services.AddScoped<BeyadAmi.Server.Application.Validators.CreateBranchValidator>();
            services.AddScoped<BeyadAmi.Server.Application.Validators.CreateDeviceValidator>();
            services.AddScoped<BeyadAmi.Server.Application.Validators.CreateStoreValidator>();
            services.AddScoped<BeyadAmi.Server.Application.Validators.CreateDeviceCategoryValidator>();

            // Register application services here so the Api project only needs to call infrastructure registration
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IDeviceCategoryService, DeviceCategoryService>();

            return services;
        }
    }
}
