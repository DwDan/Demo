namespace Demo.Todos.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    ITodoRepository Todos { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
