using Backtracer.Application.Services;

namespace Backtracer.Api.Extensions;

internal static class DependencyInjectionExtension {
    public static void InjectDependencies(this IServiceCollection services) {
        services.AddScoped<IConstantsService, ConstantsService>();
    }
}
