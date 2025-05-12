using Demo.Common.Application.Validation;
using Demo.Todos.Application;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Todos.IoC.ModuleInitializers;

public class ApplicationModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        var applicationAssembly = typeof(ApplicationLayer).Assembly;

        builder.Services.AddAutoMapper(applicationAssembly);

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(applicationAssembly);
        });

        builder.Services.AddValidatorsFromAssembly(applicationAssembly);

        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}