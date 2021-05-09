using Microsoft.Extensions.DependencyInjection;

namespace QuickHelpers.Business.CSharpExecutorService
{
    public static class Entry
    {
        public static IServiceCollection ConfigureCSharpServices(this IServiceCollection services)
        {
            services.AddSingleton<CSharpRuntimeExecutor>();
            return services;
        }
    }
}