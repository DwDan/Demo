using AutoMapper;
using Demo.Todos.Application.Todos.UpdateTodo;
using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Repositories;
using Demo.Todos.Tests.Faker;
using FluentAssertions;
using FluentValidation;
using NSubstitute;

namespace Demo.Todos.Tests.Unit.Handlers;

public class UpdateTodoHandlerTests
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    private readonly UpdateTodoHandler _handler;

    public UpdateTodoHandlerTests()
    {
        _todoRepository = Substitute.For<ITodoRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateTodoHandler(_todoRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid todo data When updating todo Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        var command = UpdateTodoHandlerTestData.GenerateValidCommand();
        var todo = UpdateTodoHandlerTestData.GenerateValidCommand(command);
        var result = new UpdateTodoResult { Id = todo.Id };

        _mapper.Map<Todo>(command).Returns(todo);
        _mapper.Map<UpdateTodoResult>(todo).Returns(result);
        _todoRepository.UpdateAsync(Arg.Any<Todo>(), Arg.Any<CancellationToken>()).Returns(todo);

        var updateTodoResult = await _handler.Handle(command, CancellationToken.None);

        updateTodoResult.Should().NotBeNull();
        updateTodoResult.Id.Should().Be(todo.Id);
        await _todoRepository.Received(1).UpdateAsync(Arg.Any<Todo>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid todo data When updating todo Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        var command = new UpdateTodoCommand();

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given valid command When handling Then maps command to todo entity")]
    public async Task Handle_ValidRequest_MapsCommandToTodo()
    {
        var command = UpdateTodoHandlerTestData.GenerateValidCommand();
        var todo = UpdateTodoHandlerTestData.GenerateValidCommand(command);

        _mapper.Map<Todo>(command).Returns(todo);
        _todoRepository.UpdateAsync(Arg.Any<Todo>(), Arg.Any<CancellationToken>()).Returns(todo);

        await _handler.Handle(command, CancellationToken.None);

        _mapper.Received(1).Map<Todo>(command);
    }
}
