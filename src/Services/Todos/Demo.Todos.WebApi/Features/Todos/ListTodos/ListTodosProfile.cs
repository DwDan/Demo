using AutoMapper;
using Demo.Common.WebApi;
using Demo.Todos.Application.Todos.Common;
using Demo.Todos.Application.Todos.ListTodos;
using Demo.Todos.WebApi.Features.Todos.Common;

namespace Demo.Todos.WebApi.Features.Todos.ListTodos;

public class ListTodosProfile : Profile
{
    public ListTodosProfile()
    {
        CreateMap<TodoApplication, TodoPresentation>()
            .ReverseMap();

        CreateMap<ApiQueryRequestPresentation, ListTodosCommand>();
        CreateMap<ListTodosResult, ListTodosResponse>();
    }
}
