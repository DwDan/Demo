using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Demo.Common.Domain;
using Demo.Common.Infrastructure;
using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Demo.Todos.Infrastructure.Repositories;

/// <summary>
/// Implementation of ITodoRepository using Entity Framework Core
/// </summary>
public class TodoRepository : ITodoRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of TodoRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public TodoRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new todo in the database
    /// </summary>
    /// <param name="todo">The todo to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created todo</returns>
    public async Task<Todo> CreateAsync(Todo todo, CancellationToken cancellationToken = default)
    {
        await _context.Todos.AddAsync(todo, cancellationToken);
        return todo;
    }

    /// <summary>
    /// Updates an existing todo in the database
    /// </summary>
    /// <param name="todo">The todo to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated todo</returns>
    public async Task<Todo> UpdateAsync(Todo todo, CancellationToken cancellationToken = default)
    {
        _context.Todos.Update(todo);
        return await Task.FromResult(todo);
    }

    /// <summary>
    /// Retrieves a todo by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the todo</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The todo if found, null otherwise</returns>
    public async Task<Todo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Todos.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a todo by a given predicate
    /// </summary>
    /// <param name="predicate">The predicate to filter todos</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The todo if found, null otherwise</returns>
    public async Task<Todo?> GetByAsync(Expression<Func<Todo, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Todos.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    /// <summary>
    /// Retrieves all todos in the database with pagination and sorting.
    /// </summary>
    /// <param name="request">The pagination and sorting request parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated response containing the list of todos</returns>
    public async Task<ApiQueryResponseDomain<Todo>> GetAllTodosAsync(ApiQueryRequestDomain request, CancellationToken cancellationToken = default)
    {
        var query = _context.Todos
            .ApplyFilters(request.Filters)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Order))
            query = query.OrderBy(request.Order.Trim());

        int totalItems = await query.CountAsync(cancellationToken);

        var data = await query
            .Skip((request.Page - 1) * request.Size)
            .Take(request.Size)
            .ToListAsync(cancellationToken);

        return new ApiQueryResponseDomain<Todo>
        {
            Data = data,
            TotalItems = totalItems,
            CurrentPage = request.Page,
            TotalPages = (int)Math.Ceiling(totalItems / (double)request.Size)
        };
    }

    /// <summary>
    /// Deletes a todo from the database by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the todo to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the todo was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var todo = await GetByIdAsync(id, cancellationToken);
        if (todo == null)
            return false;

        _context.Todos.Remove(todo);
        return true;
    }
}
