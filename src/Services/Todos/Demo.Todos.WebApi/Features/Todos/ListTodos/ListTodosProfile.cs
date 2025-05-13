using AutoMapper;
using Demo.Common.WebApi;
using Demo.Todos.Application.Todos.ListTodos;

namespace Demo.Todos.WebApi.Features.Todos.ListTodos;

public class ListTodosProfile : Profile
{
    public ListTodosProfile()
    {
        CreateMap<TodoApplication, TodoPresentation>().ReverseMap();

        CreateMap<ApiQueryRequestPresentation, ListTodosCommand>();

        CreateMap<ListTodosResult, ListTodosResponse>();
    }
}
