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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly DeleteTodoHandler _handler;

    public DeleteTodoHandlerTests()
    {
        _todoRepository = Substitute.For<ITodoRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = Substitute.For<IMapper>();
        _handler = new DeleteTodoHandler(_unitOfWork, _todoRepository);
    }

    [Fact(DisplayName = "Should return true when todo is deleted")]
    public async Task DeleteTodoHandler_ShouldReturnTrue_WhenTodoExists()
    {
        var command = DeleteTodoHandlerTestData.GenerateValidCommand();

        _todoRepository.DeleteAsync(command.Id, CancellationToken.None).Returns(true);

        var deleteTodoResponse = await _handler.Handle(command, CancellationToken.None);

        deleteTodoResponse.Should().NotBeNull();
        deleteTodoResponse.Success.Should().BeTrue();

        await _todoRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Should throw KeyNotFoundException when todo does not exist")]
    public async Task DeleteTodoHandler_Throw_KeyNotFoundException_WhenTodoDoesNotExist()
    {
        var command = DeleteTodoHandlerTestData.GenerateValidCommand();

        _todoRepository.DeleteAsync(command.Id, CancellationToken.None).Returns(false);

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        exception.Message.Should().Be($"Todo with ID {command.Id} not found");

        await _todoRepository.Received(1).DeleteAsync(command.Id, CancellationToken.None);
        await _unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }
}
