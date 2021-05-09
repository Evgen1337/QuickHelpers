using Microsoft.Extensions.DependencyInjection;

namespace QuickHelpers.Business.ConvertorService
{
    public static class Entry
    {
        public static IServiceCollection ConfigureConvertors(this IServiceCollection services)
        {
            services.AddSingleton<Convertor>();
            return services;
        }
    }
}