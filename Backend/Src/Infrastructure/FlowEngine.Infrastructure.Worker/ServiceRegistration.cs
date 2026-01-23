using FlowEngine.Application.Interfaces;
using FlowEngine.Infrastructure.Worker.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FlowEngine.Infrastructure.Worker
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddWorkerInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<FlowEngineContext>();
            services.AddScoped<IFlowEngineServices, FlowEngineServices>();

            return services;
        }
    }
}
