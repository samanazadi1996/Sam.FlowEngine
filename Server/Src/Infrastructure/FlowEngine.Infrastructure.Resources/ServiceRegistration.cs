using FlowEngine.Application.Interfaces;
using FlowEngine.Infrastructure.Resources.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FlowEngine.Infrastructure.Resources
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddResourcesInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ITranslator, Translator>();

            return services;
        }
    }
}
