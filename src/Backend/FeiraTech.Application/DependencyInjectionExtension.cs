using FeiraTech.Application.Services.AutoMapper;
using FeiraTech.Application.Services.Criptography;
using FeiraTech.Application.UseCase.User.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeiraTech.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AutoMapping));
            UseCase(services);
            PasswordHash(services, configuration);

        }

        public static void UseCase(IServiceCollection services)
        {
               services.AddScoped<IUserRegisterUseCase, UserRegisterUseCase>();
            
        }

        public static void PasswordHash(IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Services:Password:AdditionalKey");
            services.AddScoped(options => new PasswordEncripter(additionalKey!));

        }




    }

}
