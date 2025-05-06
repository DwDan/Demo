using AutoMapper;
using Demo.Todos.Domain.Entities;

namespace Demo.Todos.Application.Todos.UpdateTodo;

public class UpdateTodoProfile : Profile
{
    public UpdateTodoProfile()
    {
        CreateMap<UpdateTodoCommand, Todo>();
        CreateMap<Todo, UpdateTodoResult>();
    }
}
