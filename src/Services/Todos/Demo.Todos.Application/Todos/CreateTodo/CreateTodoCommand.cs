using Demo.Todos.Domain.Enums;
using MediatR;

namespace Demo.Todos.Application.Todos.CreateTodo;

public class CreateTodoCommand : IRequest<CreateTodoResult>
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public TodoStatus Status { get; set; } = TodoStatus.Pending;

    public DateTime DueDate { get; set; }
}