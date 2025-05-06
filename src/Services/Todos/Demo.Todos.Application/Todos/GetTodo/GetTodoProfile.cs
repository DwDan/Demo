using AutoMapper;
using Demo.Todos.Domain.Entities;

namespace Demo.Todos.Application.Todos.GetTodo;

public class GetTodoProfile : Profile
{
    public GetTodoProfile()
    {
        CreateMap<Todo, GetTodoResult>();
    }
}
