using Demo.Todos.IoC.ModuleInitializers;
using Microsoft.AspNetCore.Builder;

namespace Demo.Todos.IoC;

public static class DependencyResolver
{
    public static void RegisterDependencies(this WebApplicationBuilder builder)
    {
        new InfrastructureModuleInitializer().Initialize(builder);
        new WebApiModuleInitializer().Initialize(builder);
    }
}