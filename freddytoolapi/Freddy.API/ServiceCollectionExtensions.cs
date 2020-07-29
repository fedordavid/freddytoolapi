using Freddy.API.Core;
using Freddy.API.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Freddy.API
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddApi(this IServiceCollection services)
        {
            services.TryAddSingleton<IGuidProvider, GuidProvider>();
            
            return services.AddControllers(mvc => mvc.Filters.Add(typeof(NotFoundExceptionReturns404Filter)));
        }
    }
}