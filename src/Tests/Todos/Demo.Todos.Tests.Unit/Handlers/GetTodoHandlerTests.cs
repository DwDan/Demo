using AutoMapper;
using Demo.Todos.Application.Todos.GetTodo;
using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Repositories;
using Demo.Todos.Tests.Faker;
using FluentAssertions;
using NSubstitute;

namespace Demo.Todos.Tests.Unit.Handlers;

public class GetTodoHandlerTests
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    private readonly GetTodoHandler _handler;

    public GetTodoHandlerTests()
    {
        _todoRepository = Substitute.For<ITodoRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetTodoHandler(_todoRepository, _mapper);
    }

    [Fact(DisplayName = "Should return todo when todo exists")]
    public async Task GetTodoHandler_ShouldReturnTodo_WhenTodoExists()
    {
        // Arrange
        var command = GetTodoHandlerTestData.GenerateValidCommand();
        var todo = TodoHandlerTestData.GenerateValidEntity();
        var result = new GetTodoResult { Id = todo.Id };

        _todoRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(todo);
        _mapper.Map<GetTodoResult>(todo).Returns(result);

        // Act
        var getTodoResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        getTodoResult.Should().NotBeNull();
        getTodoResult.Id.Should().Be(todo.Id);
        await _todoRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Should throw KeyNotFoundException when todo does not exist")]
    public async Task GetTodoHandler_Throws_KeyNotFoundException_WhenTodoDoesNotExist()
    {
        // Arrange
        var command = GetTodoHandlerTestData.GenerateValidCommand();
        _todoRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Todo)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        exception.Message.Should().Be($"Todo with ID {command.Id} not found");
        await _todoRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
    }
}
