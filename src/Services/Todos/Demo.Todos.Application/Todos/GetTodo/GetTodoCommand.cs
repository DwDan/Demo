using MediatR;

namespace Demo.Todos.Application.Todos.GetTodo;

public record GetTodoCommand : IRequest<GetTodoResult>
{
    public int Id { get; }

    public GetTodoCommand(int id)
    {
        Id = id;
    }
}
