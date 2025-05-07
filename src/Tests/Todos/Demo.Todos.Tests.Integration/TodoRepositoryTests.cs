using Demo.Common.Domain;
using Demo.Todos.Infrastructure.Repositories;
using Demo.Todos.Tests.Faker;
using FluentAssertions;

namespace Demo.Todos.Tests.Functional;

public class TodoRepositoryTests : BaseRepositoryTests
{
    public TodoRepositoryTests(IntegrationDatabaseFixture fixture) : base(fixture) { }

    [Fact(DisplayName = "Given a new todo When adding to repository Then should persist in database")]
    public async Task AddTodo_ShouldPersistInDatabase()
    {
        var repository = new TodoRepository(DbContext);
        var todo = TodoHandlerTestData.GenerateValidEntity();

        await repository.CreateAsync(todo);
        await DbContext.SaveChangesAsync();

        var result = await repository.GetByIdAsync(todo.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(todo.Id);
    }

    [Fact(DisplayName = "Given an existing todo When updating Then should update correctly")]
    public async Task UpdateTodo_ShouldUpdateCorrectly()
    {
        var repository = new TodoRepository(DbContext);
        var todo = TodoHandlerTestData.GenerateValidEntity();

        await repository.CreateAsync(todo);
        await DbContext.SaveChangesAsync();

        todo.Title = "Updated Todo";
        await repository.UpdateAsync(todo);
        await DbContext.SaveChangesAsync();

        var result = await repository.GetByIdAsync(todo.Id);

        result.Should().NotBeNull();
        result.Title.Should().Be("Updated Todo");
    }

    [Fact(DisplayName = "Given a todo ID When retrieving Then should return correct todo")]
    public async Task GetTodoById_ShouldReturnCorrectTodo()
    {
        var repository = new TodoRepository(DbContext);
        var todo = TodoHandlerTestData.GenerateValidEntity();

        await repository.CreateAsync(todo);
        await DbContext.SaveChangesAsync();

        var result = await repository.GetByIdAsync(todo.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(todo.Id);
    }

    [Fact(DisplayName = "Given a predicate When retrieving Then should return correct todo")]
    public async Task GetTodoByPredicate_ShouldReturnCorrectTodo()
    {
        var repository = new TodoRepository(DbContext);
        var todo = TodoHandlerTestData.GenerateValidEntity();

        await repository.CreateAsync(todo);
        await DbContext.SaveChangesAsync();

        var result = await repository.GetByAsync(b => b.Title == todo.Title);

        result.Should().NotBeNull();
        result.Title.Should().Be(todo.Title);
    }

    [Fact(DisplayName = "Given a pagination request When retrieving all todos Then should return paginated result")]
    public async Task GetAllTodos_ShouldReturnPaginatedResult()
    {
        var repository = new TodoRepository(DbContext);
        var todo1 = TodoHandlerTestData.GenerateValidEntity();
        var todo2 = TodoHandlerTestData.GenerateValidEntity();

        await repository.CreateAsync(todo1);
        await repository.CreateAsync(todo2);
        await DbContext.SaveChangesAsync();

        var request = new ApiQueryRequestDomain { Page = 1, Size = 10 };
        var result = await repository.GetAllTodosAsync(request);

        result.Should().NotBeNull();
        result.Data.Should().NotBeEmpty();
        result.TotalItems.Should().Be(2);
    }

    [Fact(DisplayName = "Given a todo ID When deleting Then should remove todo correctly")]
    public async Task DeleteTodo_ShouldRemoveTodoCorrectly()
    {
        var repository = new TodoRepository(DbContext);
        var todo = TodoHandlerTestData.GenerateValidEntity();

        await repository.CreateAsync(todo);
        await DbContext.SaveChangesAsync();

        var deleted = await repository.DeleteAsync(todo.Id);
        await DbContext.SaveChangesAsync();

        var result = await repository.GetByIdAsync(todo.Id);

        deleted.Should().BeTrue();
        result.Should().BeNull();
    }
}
