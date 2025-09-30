using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GenericRepository;
using Scrutor;
using KullaniciYonetimi.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using KullaniciYonetimi.Domain.Entities;

namespace KullaniciYonetimi.Infrastructure
{
    public static class InfrastructureRegistrar
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                string connection = configuration.GetConnectionString("PostgreSql")!;
                opt.UseNpgsql(connection);
            });

            //Identity
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            // UnitOfWork
            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<AppDbContext>());

            // Repositories
            services.Scan(opt => opt
          .FromAssemblies(typeof(InfrastructureRegistrar).Assembly)
          .AddClasses(publicOnly: false)
          .UsingRegistrationStrategy(RegistrationStrategy.Skip)
          .AsImplementedInterfaces()
          .WithScopedLifetime()
          );

            return services;
        }
    }
}
