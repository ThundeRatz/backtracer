using Backtracer.Application.Services;

namespace Backtracer.Api.Extensions;

internal static class DependencyInjectionExtension {
    public static void AddServices(this IServiceCollection services) {
        services.AddScoped<IConstantsService, ConstantsService>();
        // services.AddScoped<ILapsService, LapsService>();
    }
}
