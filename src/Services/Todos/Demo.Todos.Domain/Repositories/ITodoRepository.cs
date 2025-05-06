using System.Linq.Expressions;
using Demo.Common.Domain;
using Demo.Todos.Domain.Entities;

namespace Demo.Todos.Domain.Repositories;

/// <summary>
/// Repository interface for Todo entity operations
/// </summary>
public interface ITodoRepository
{
    /// <summary>
    /// Creates a new todo in the repository
    /// </summary>
    /// <param name="todo">The todo to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created todo</returns>
    Task<Todo> CreateAsync(Todo todo, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing todo in the repository
    /// </summary>
    /// <param name="todo">The todo to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated todo</returns>
    Task<Todo> UpdateAsync(Todo todo, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a todo by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the todo</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The todo if found, null otherwise</returns>
    Task<Todo?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a todo by predicate
    /// </summary>
    /// <param name="predicate">The predicate to filter todos</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The todo if found, null otherwise</returns>
    Task<Todo?> GetByAsync(Expression<Func<Todo, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all todos with pagination and sorting
    /// </summary>
    /// <param name="request">The pagination and sorting request parameters</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
    /// <returns>A paginated response containing the list of Todos</returns>
    Task<ApiQueryResponseDomain<Todo>> GetAllTodosAsync(ApiQueryRequestDomain request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a todo from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the todo to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the todo was deleted, false if not found</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
