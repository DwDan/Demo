using Demo.Common.Application.Validation;
using Demo.Todos.Application;
using Demo.Todos.Infrastructure;
using Demo.Todos.IoC;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();


try
{
    Log.Information("Starting web application");

    var app = CreateWebApplication(args);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}


static WebApplication CreateWebApplication(string[] args)
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    //builder.AddDefaultLogging();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    //builder.AddBasicHealthChecks();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<DefaultContext>(options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("Demo.Todos.Infrastructure")
        )
    );

    //builder.Services.AddJwtAuthentication(builder.Configuration);

    builder.RegisterDependencies();

    builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblies(
            typeof(ApplicationLayer).Assembly,
            typeof(Program).Assembly
        );
    });

    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend",
            policy => policy.WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials());
    });

    var app = builder.Build();
    //app.UseMiddleware<ValidationExceptionMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("AllowFrontend");

    app.UseAuthentication();
    app.UseAuthorization();

    //app.UseBasicHealthChecks();

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
        
        dbContext.Database.Migrate();
    }

    return app;
}