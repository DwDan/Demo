using AutoMapper;
using Demo.Todos.Application.Todos.CreateTodo;
using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Repositories;
using Demo.Todos.Tests.Faker;
using FluentAssertions;
using NSubstitute;

namespace Demo.Todos.Tests.Unit.Handlers;

public class CreateTodoHandlerTests
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CreateTodoHandler _handler;

    public CreateTodoHandlerTests()
    {
        _todoRepository = Substitute.For<ITodoRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateTodoHandler(_unitOfWork, _todoRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid todo data When creating todo Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateTodoHandlerTestData.GenerateValidCommand();
        var todo = CreateTodoHandlerTestData.GenerateValidCommand(command);
        var result = new CreateTodoResult { Id = todo.Id };

        _mapper.Map<Todo>(command).Returns(todo);
        _mapper.Map<CreateTodoResult>(todo).Returns(result);
        _todoRepository.CreateAsync(todo, Arg.Any<CancellationToken>()).Returns(todo);

        // When
        var createTodoResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createTodoResult.Should().NotBeNull();
        createTodoResult.Id.Should().Be(todo.Id);

        await _todoRepository.Received(1).CreateAsync(todo, Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid command When handling Then maps command to todo entity")]
    public async Task Handle_ValidRequest_MapsCommandToTodo()
    {
        // Given
        var command = CreateTodoHandlerTestData.GenerateValidCommand();
        var todo = CreateTodoHandlerTestData.GenerateValidCommand(command);

        _mapper.Map<Todo>(command).Returns(todo);
        _todoRepository.CreateAsync(todo, Arg.Any<CancellationToken>()).Returns(todo);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Todo>(command);
        await _todoRepository.Received(1).CreateAsync(todo, Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }
}
