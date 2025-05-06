using AutoMapper;
using Demo.Common.Domain;
using Demo.Todos.Application.Todos.Common;
using Demo.Todos.Domain.Entities;

namespace Demo.Todos.Application.Todos.ListTodos;

public class ListTodosProfile : Profile
{
    public ListTodosProfile()
    {
        CreateMap<Todo, TodoApplication>()
           .ReverseMap();

        CreateMap<ListTodosCommand, ApiQueryRequestDomain>();

        CreateMap<ApiQueryResponseDomain<Todo>, ListTodosResult>();
    }
}
