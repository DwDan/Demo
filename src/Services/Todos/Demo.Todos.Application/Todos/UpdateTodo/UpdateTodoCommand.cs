using Demo.Todos.Domain.Enums;
using MediatR;

namespace Demo.Todos.Application.Todos.UpdateTodo;

public class UpdateTodoCommand : IRequest<UpdateTodoResult> 
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public TodoStatus Status { get; set; } = TodoStatus.Pending;

    public DateTime DueDate { get; set; }
}