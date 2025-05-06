using AutoMapper;
using Demo.Todos.Application.Todos.DeleteTodo;

namespace Demo.Todos.WebApi.Features.Todos.DeleteTodo;

public class DeleteTodoProfile : Profile
{
    public DeleteTodoProfile()
    {
        CreateMap<int, DeleteTodoCommand>()
            .ConstructUsing(id => new DeleteTodoCommand(id));
    }
}
