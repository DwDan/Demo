using AutoMapper;
using Demo.Todos.Application.Todos.DeleteTodo;
using Demo.Todos.Domain.Repositories;
using Demo.Todos.Tests.Faker;
using FluentAssertions;
using NSubstitute;

namespace Demo.Todos.Tests.Unit.Handlers;

/// <summary>
/// Contains unit tests for the <see cref="DeleteTodoHandler"/> class.
/// </summary>
public class DeleteTodoHandlerTests
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    private readonly DeleteTodoHandler _handler;

    public DeleteTodoHandlerTests()
    {
        _todoRepository = Substitute.For<ITodoRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new DeleteTodoHandler(_todoRepository);
    }

    [Fact(DisplayName = "Should return true when user deleted")]
    public async Task DeleteTodoHandler_ShouldReturnTodo_WhenTodoExists()
    {
        var command = DeleteTodoHandlerTestData.GenerateValidCommand();
        var user = TodoHandlerTestData.GenerateValidEntity();

        _todoRepository.DeleteAsync(command.Id, CancellationToken.None).Returns(true);

        var deleteTodoResponse = await _handler.Handle(command, CancellationToken.None);

        deleteTodoResponse.Should().NotBeNull();
        deleteTodoResponse.Success.Should().Be(true);
        await _todoRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
    }

    [Fact(DisplayName = "Should throw KeyNotFoundException when user does not exist")]
    public async Task DeleteTodoHandler_Throw_KeyNotFoundException_WhenTodoDoesNotExist()
    {
        var command = DeleteTodoHandlerTestData.GenerateValidCommand();
        var user = TodoHandlerTestData.GenerateValidEntity();

        _todoRepository.DeleteAsync(command.Id, CancellationToken.None).Returns(false);

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal($"Todo with ID {command.Id} not found", exception.Message);

        await _todoRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
    }

    [Fact(DisplayName = "Should throws ValidationException when id was not provide")]
    public async Task DeleteTodoHandler_Throw_ValidationException_WhenTodoDoesNotExist()
    {
        var command = new DeleteTodoCommand(0);
        var user = TodoHandlerTestData.GenerateValidEntity();

        var exception = await Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Validation failed: \r\n -- Id: Todo ID is required Severity: Error", exception.Message);
    }
}
