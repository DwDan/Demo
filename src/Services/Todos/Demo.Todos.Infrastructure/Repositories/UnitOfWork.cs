using Demo.Todos.Domain.Repositories;

namespace Demo.Todos.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DefaultContext _context;
    private ITodoRepository _todos;

    public UnitOfWork(DefaultContext context)
    {
        _context = context;
    }

    public ITodoRepository Todos => _todos ??= new TodoRepository(_context);

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}