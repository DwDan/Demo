using Demo.Todos.Application.Todos.Common;
using MediatR;

namespace Demo.Todos.Application.Todos.UpdateTodo;

public class UpdateTodoCommand : TodoApplication, IRequest<UpdateTodoResult> { }