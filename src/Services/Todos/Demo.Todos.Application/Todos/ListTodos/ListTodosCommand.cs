using Demo.Common.Application;
using MediatR;

namespace Demo.Todos.Application.Todos.ListTodos;

public record ListTodosCommand : ApiQueryRequestApplication, IRequest<ListTodosResult> { }
