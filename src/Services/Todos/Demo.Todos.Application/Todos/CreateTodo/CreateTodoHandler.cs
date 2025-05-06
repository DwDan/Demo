using AutoMapper;
using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Demo.Todos.Application.Todos.CreateTodo;

public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, CreateTodoResult>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public CreateTodoHandler(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<CreateTodoResult> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateTodoCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var todo = _mapper.Map<Todo>(command);

        var createdUser = await _todoRepository.CreateAsync(todo, cancellationToken);
        var result = _mapper.Map<CreateTodoResult>(createdUser);
        return result;
    }
}
