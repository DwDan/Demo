using Bogus;
using Demo.Todos.Application.Todos.GetTodo;

namespace Demo.Todos.Tests.Faker;

public static class GetTodoHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid GetTodoCommand instances.
    /// The generated commands will have valid IDs.
    /// </summary>
    private static readonly Faker<GetTodoCommand> getTodoHandlerFaker = new Faker<GetTodoCommand>()
        .CustomInstantiator(f => new GetTodoCommand(f.Random.Int(1, 99)));

    /// <summary>
    /// Generates a valid GetTodoCommand with randomized data.
    /// </summary>
    /// <returns>A valid GetTodoCommand instance.</returns>
    public static GetTodoCommand GenerateValidCommand()
    {
        return getTodoHandlerFaker.Generate();
    }
}
