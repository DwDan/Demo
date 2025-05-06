using MediatR;

namespace Demo.Todos.Application.Todos.DeleteTodo;

public record DeleteTodoCommand : IRequest<DeleteTodoResponse>
{
    public int Id { get; }

    public DeleteTodoCommand(int id)
    {
        Id = id;
    }
}
