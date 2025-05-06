using AutoMapper;
using Demo.Todos.Domain.Entities;

namespace Demo.Todos.Application.Todos.CreateTodo;

public class CreateTodoProfile : Profile
{
    public CreateTodoProfile()
    {
        CreateMap<CreateTodoCommand, Todo>();
        CreateMap<Todo, CreateTodoResult>();
    }
}
