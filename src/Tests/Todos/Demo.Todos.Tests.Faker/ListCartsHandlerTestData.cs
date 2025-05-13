using Bogus;
using Demo.Common.Domain;
using Demo.Todos.Application.Todos.ListTodos;
using Demo.Todos.Domain.Entities;

namespace Demo.Todos.Tests.Faker;

public static class ListTodosHandlerTestData
{
    private static readonly Faker<ListTodosCommand> listTodosHandlerFaker = new Faker<ListTodosCommand>()
        .RuleFor(u => u.Page, f => f.Random.Number(1, 99))
        .RuleFor(u => u.Size, f => f.Random.Number(1, 99))
        .RuleFor(u => u.Order, f => f.GenerateOrder<Todo>());

    public static ListTodosCommand GenerateValidCommand()
    {
        return listTodosHandlerFaker.Generate();
    }

    public static ApiQueryResponseDomain<Todo> GenerateValidResponse()
    {
        return new ApiQueryResponseDomain<Todo>
        {
            Data = new List<Todo>() { TodoHandlerTestData.GenerateValidEntity() },
            TotalItems = 1,
            CurrentPage = 1,
            TotalPages = 1
        };
    }
}
