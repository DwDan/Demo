using AutoMapper;
using Demo.Todos.Application.Todos.CreateTodo;

namespace Demo.Todos.WebApi.Features.Todos.CreateTodo;

public class CreateTodoProfile : Profile
{
    public CreateTodoProfile()
    {        
        CreateMap<CreateTodoRequest, CreateTodoCommand>();
        CreateMap<CreateTodoResult, CreateTodoResponse>();
    }
}
