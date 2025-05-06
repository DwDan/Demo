using AutoMapper;
using Demo.Todos.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Demo.Todos.Application.Todos.GetTodo;

public class GetTodoHandler : IRequestHandler<GetTodoCommand, GetTodoResult>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public GetTodoHandler(
        ITodoRepository todoRepository,
        IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<GetTodoResult> Handle(GetTodoCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetTodoValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var todo = await _todoRepository.GetByIdAsync(request.Id, cancellationToken);
        if (todo == null)
            throw new KeyNotFoundException($"Todo with ID {request.Id} not found");

        return _mapper.Map<GetTodoResult>(todo);
    }
}
