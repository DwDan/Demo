using Demo.Todos.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Demo.Todos.Application.Todos.DeleteTodo;

public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, DeleteTodoResponse>
{
    private readonly ITodoRepository _todoRepository;

    public DeleteTodoHandler(
        ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<DeleteTodoResponse> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteTodoValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _todoRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Todo with ID {request.Id} not found");

        return new DeleteTodoResponse { Success = true };
    }
}
