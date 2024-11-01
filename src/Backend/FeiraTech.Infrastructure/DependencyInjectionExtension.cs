﻿using FeiraTech.Domain.Repositorie.User;
using FeiraTech.Infrastructure.DataAcess;
using FeiraTech.Infrastructure.DataAcess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeiraTech.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositorie(services);
            AddDbContext_MySql(services, configuration);
        }
        public static void AddRepositorie(IServiceCollection services)
        {
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        }

        public static void AddDbContext_MySql(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("FeiraTechDbContext");
            services.AddDbContext<FeiraTechDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        }

    }
}