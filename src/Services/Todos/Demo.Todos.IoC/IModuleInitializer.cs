using Microsoft.AspNetCore.Builder;

namespace Demo.Todos.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}
