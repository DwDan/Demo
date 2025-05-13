using AutoMapper;
using Demo.Todos.Application.Todos.GetTodo;

namespace Demo.Todos.WebApi.Features.Todos.GetTodo;

public class GetTodoProfile : Profile
{
    public GetTodoProfile()
    {
        CreateMap<GetTodoResult, GetTodoResponse>();
    }
}