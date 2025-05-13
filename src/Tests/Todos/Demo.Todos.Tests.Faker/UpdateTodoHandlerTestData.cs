using Bogus;
using Demo.Todos.Application.Todos.UpdateTodo;
using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Enums;

namespace Demo.Todos.Tests.Faker;

public static class UpdateTodoHandlerTestData
{
    private static readonly Faker<UpdateTodoCommand> updateTodoHandlerFaker = new Faker<UpdateTodoCommand>()
        .RuleFor(c => c.Id, f => f.Random.Int(1, 1000))
        .RuleFor(c => c.Title, f => f.Lorem.Sentence(3))
        .RuleFor(c => c.Description, f => f.Lorem.Paragraph())
        .RuleFor(c => c.Status, f => f.PickRandom<TodoStatus>())
        .RuleFor(c => c.DueDate, f => f.Date.Future());

    public static UpdateTodoCommand GenerateValidCommand()
    {
        return updateTodoHandlerFaker.Generate();
    }

    public static Todo GenerateValidCommand(UpdateTodoCommand command)
    {
        var todo = new Todo(command.Title, command.Description, command.DueDate);

        todo.SetStatusForTests(command.Status);
        todo.SetIdForTests(command.Id);
        return todo;
    }
}
