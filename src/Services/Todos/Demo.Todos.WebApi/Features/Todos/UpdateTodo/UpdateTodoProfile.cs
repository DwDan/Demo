using AutoMapper;
using Demo.Todos.Application.Todos.UpdateTodo;

namespace Demo.Todos.WebApi.Features.Todos.UpdateTodo;

public class UpdateTodoProfile : Profile
{
    public UpdateTodoProfile()
    {
        CreateMap<UpdateTodoRequest, UpdateTodoCommand>();

        CreateMap<UpdateTodoResult, UpdateTodoResponse>();
    }
}
