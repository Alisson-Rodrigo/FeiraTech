using FeiraTech.Domain.Repositorie;
using FeiraTech.Domain.Repositorie.User;
using FeiraTech.Infrastructure.DataAcess;
using FeiraTech.Infrastructure.DataAcess.Repositories;
using FeiraTech.Infrastructure.Extensions;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FeiraTech.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositorie(services);
            AddDbContext_MySql(services, configuration);
            FluenteMigrator_MySql(services, configuration);
        }
        public static void AddRepositorie(IServiceCollection services)
        {
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddDbContext_MySql(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionStringData();
            services.AddDbContext<FeiraTechDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        }

        public static void FluenteMigrator_MySql(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionStringData();

            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options.AddMySql5()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.Load("FeiraTech.Infrastructure")).For.All();
            });
        }


    }
}
