using AutoMapper;
using Demo.Common.Domain;
using Demo.Todos.Application.Todos.ListTodos;
using Demo.Todos.Domain.Repositories;
using Demo.Todos.Tests.Faker;
using FluentAssertions;
using NSubstitute;

namespace Demo.Todos.Tests.Unit.Handlers;

public class ListTodosHandlerTests
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    private readonly ListTodosHandler _handler;

    public ListTodosHandlerTests()
    {
        _todoRepository = Substitute.For<ITodoRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new ListTodosHandler(_todoRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid query When listing todos Then returns list response")]
    public async Task Handle_ValidRequest_ReturnsListResponse()
    {
        // Given
        var command = ListTodosHandlerTestData.GenerateValidCommand();
        var apiQuery = new ApiQueryRequestDomain();
        var response = ListTodosHandlerTestData.GenerateValidResponse();

        _mapper.Map<ApiQueryRequestDomain>(command).Returns(apiQuery);
        _todoRepository.GetAllTodosAsync(apiQuery, Arg.Any<CancellationToken>()).Returns(response);
        _mapper.Map<ListTodosResult>(response).Returns(new ListTodosResult());

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        await _todoRepository.Received(1).GetAllTodosAsync(apiQuery, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given valid query When handling Then maps command to API query")]
    public async Task Handle_ValidRequest_MapsCommandToApiQuery()
    {
        // Given
        var command = ListTodosHandlerTestData.GenerateValidCommand();
        var apiQuery = new ApiQueryRequestDomain();
        _mapper.Map<ApiQueryRequestDomain>(command).Returns(apiQuery);

        _todoRepository.GetAllTodosAsync(apiQuery, Arg.Any<CancellationToken>()).Returns(ListTodosHandlerTestData.GenerateValidResponse());

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<ApiQueryRequestDomain>(command);
    }
}
