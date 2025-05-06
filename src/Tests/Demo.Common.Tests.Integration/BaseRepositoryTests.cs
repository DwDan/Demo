using Demo.Todos.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class BaseRepositoryTests : IClassFixture<IntegrationDatabaseFixture>, IAsyncLifetime
{
    private readonly IntegrationDatabaseFixture _fixture;

    private ServiceProvider _serviceProvider = null!;
    protected DefaultContext DbContext { get; private set; } = null!;

    public BaseRepositoryTests(IntegrationDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    public async Task InitializeAsync()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<DefaultContext>(options =>
            options.UseNpgsql(_fixture.ConnectionString));

        _serviceProvider = serviceCollection.BuildServiceProvider();
        DbContext = _serviceProvider.GetRequiredService<DefaultContext>();

        await ResetDatabaseAsync(); 
    }

    public Task DisposeAsync()
    {
        _serviceProvider.Dispose();
        return Task.CompletedTask;
    }

    private async Task ResetDatabaseAsync()
    {
        DbContext.ChangeTracker.Clear();

        await DbContext.Database.ExecuteSqlRawAsync(@"
            DO $$ 
            DECLARE 
                r RECORD;
            BEGIN 
                FOR r IN (SELECT tablename FROM pg_tables WHERE schemaname = 'public') LOOP
                    EXECUTE 'TRUNCATE TABLE ' || quote_ident(r.tablename) || ' RESTART IDENTITY CASCADE;';
                END LOOP;
            END $$;
        ");
    }
}
