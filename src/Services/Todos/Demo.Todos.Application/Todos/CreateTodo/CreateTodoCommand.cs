using Demo.Todos.Application.Todos.Common;
using MediatR;

namespace Demo.Todos.Application.Todos.CreateTodo;

public class CreateTodoCommand : TodoApplication, IRequest<CreateTodoResult>{ }