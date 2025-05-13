using Bogus;
using Demo.Todos.Application.Todos.CreateTodo;
using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Enums;

namespace Demo.Todos.Tests.Faker;

public static class CreateTodoHandlerTestData
{
    private static readonly Faker<CreateTodoCommand> createTodoHandlerFaker = new Faker<CreateTodoCommand>()
        .RuleFor(c => c.Title, f => f.Lorem.Sentence(3))
        .RuleFor(c => c.Description, f => f.Lorem.Paragraph())
        .RuleFor(c => c.Status, f => f.PickRandom<TodoStatus>())
        .RuleFor(c => c.DueDate, f => f.Date.Future());

    public static CreateTodoCommand GenerateValidCommand()
    {
        return createTodoHandlerFaker.Generate();
    }

    public static Todo GenerateValidCommand(CreateTodoCommand command)
    {
        var todo = new Todo(command.Title, command.Description, command.DueDate);

        todo.SetStatusForTests(command.Status);
        return todo;
    }
}
