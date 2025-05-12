using AutoMapper;
using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Repositories;
using MediatR;

namespace Demo.Todos.Application.Todos.UpdateTodo;

public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, UpdateTodoResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public UpdateTodoHandler(IUnitOfWork unitOfWork, ITodoRepository todoRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<UpdateTodoResult> Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _todoRepository.GetByAsync(
            todo => todo.Title == command.Title && todo.Id != command.Id,
            cancellationToken
        );

        if (existingUser != null)
            throw new InvalidOperationException($"Todo with name {command.Title} already exists");

        var todo = _mapper.Map<Todo>(command);
        await _todoRepository.UpdateAsync(todo, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<UpdateTodoResult>(todo);
    }
}
