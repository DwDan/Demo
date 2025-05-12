using AutoMapper;
using Demo.Common.Domain;
using Demo.Todos.Domain.Repositories;
using MediatR;

namespace Demo.Todos.Application.Todos.ListTodos;

public class ListTodosHandler : IRequestHandler<ListTodosCommand, ListTodosResult>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public ListTodosHandler(
        ITodoRepository todoRepository,
        IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<ListTodosResult> Handle(ListTodosCommand command, CancellationToken cancellationToken)
    {
        var apiQuery = _mapper.Map<ApiQueryRequestDomain>(command);

        var response = await _todoRepository.GetAllTodosAsync(apiQuery, cancellationToken);

        return _mapper.Map<ListTodosResult>(response);
    }
}
