using Bogus;
using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Enums;

namespace Demo.Todos.Tests.Faker;

public static class TodoHandlerTestData
{
    private static readonly Faker<Todo> handlerFaker = new Faker<Todo>()
        .CustomInstantiator(f => new Todo("title", "desc", DateTime.UtcNow.AddDays(1)))
        .RuleFor(c => c.DueDate, f => f.Date.Future().ToUniversalTime())
        .FinishWith((f, todo) =>
        {
            todo.SetIdForTests(f.Random.Int(1, 1000));
            todo.SetTitle(f.Lorem.Sentence(3));
            todo.SetDescription(f.Lorem.Paragraph());
            todo.SetStatusForTests(f.PickRandom<TodoStatus>());
        });

    public static Todo GenerateValidEntity()
    {
        return handlerFaker.Generate();
    }       
}