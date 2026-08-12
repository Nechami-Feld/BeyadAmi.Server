using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BeyadAmi.Server.Infrastructure.Persistence;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Infrastructure.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Services;
using BeyadAmi.Server.Application.Interfaces;
using BeyadAmi.Server.Infrastructure.Security;
using BeyadAmi.Server.Infrastructure.Services;

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
            services.AddScoped<IBranchRequestRepository, BranchRequestRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Authentication related
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            // Validators
            services.AddScoped<BeyadAmi.Server.Application.Validators.CreateBranchValidator>();
            services.AddScoped<BeyadAmi.Server.Application.Validators.CreateDeviceValidator>();
            services.AddScoped<BeyadAmi.Server.Application.Validators.CreateStoreValidator>();
            services.AddScoped<BeyadAmi.Server.Application.Validators.CreateDeviceCategoryValidator>();
            services.AddScoped<BeyadAmi.Server.Application.Validators.CreateBranchRequestValidator>();
            services.AddScoped<BeyadAmi.Server.Application.Validators.CreateLoanValidator>();
            services.AddScoped<BeyadAmi.Server.Application.Validators.CreatePurchaseValidator>();
            services.AddScoped<BeyadAmi.Server.Application.Validators.CreateProductValidator>();
            services.AddScoped<BeyadAmi.Server.Application.Validators.RegisterRequestValidator>();

            // Register application services here so the Api project only needs to call infrastructure registration
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IDeviceCategoryService, DeviceCategoryService>();
            services.AddScoped<IBranchRequestService, BranchRequestService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
