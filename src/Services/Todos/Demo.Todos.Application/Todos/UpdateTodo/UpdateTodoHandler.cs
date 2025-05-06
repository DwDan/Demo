using AutoMapper;
using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Demo.Todos.Application.Todos.UpdateTodo;

public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, UpdateTodoResult>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public UpdateTodoHandler(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<UpdateTodoResult> Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateTodoCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingUser = await _todoRepository.GetByAsync((todo)=> todo.Name == command.Name && todo.Id != command.Id, cancellationToken);
        if (existingUser != null)
            throw new InvalidOperationException($"Todo with name {command.Name} already exists");

        var todo = _mapper.Map<Todo>(command);

        var createdUser = await _todoRepository.UpdateAsync(todo, cancellationToken);
        var result = _mapper.Map<UpdateTodoResult>(createdUser);
        return result;
    }
}
