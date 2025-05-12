using AutoMapper;
using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Repositories;
using MediatR;

namespace Demo.Todos.Application.Todos.CreateTodo;

public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, CreateTodoResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public CreateTodoHandler(IUnitOfWork unitOfWork, ITodoRepository todoRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<CreateTodoResult> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = _mapper.Map<Todo>(command);
        await _todoRepository.CreateAsync(todo, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<CreateTodoResult>(todo);
    }
}
