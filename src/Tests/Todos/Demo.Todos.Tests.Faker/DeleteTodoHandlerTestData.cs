using Bogus;
using Demo.Todos.Application.Todos.DeleteTodo;

namespace Demo.Todos.Tests.Faker;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class DeleteTodoHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Todo entities.
    /// The generated todos will have valid:
    /// - Id (using random numbers)
    /// </summary>
    private static readonly Faker<DeleteTodoCommand> deleteTodoHandlerFaker = new Faker<DeleteTodoCommand>()
        .CustomInstantiator(f => new DeleteTodoCommand(f.Random.Int(1, 99)));

    /// <summary>
    /// Generates a valid Todo entity with randomized data.
    /// The generated todo will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Todo entity with randomly generated data.</returns>
    public static DeleteTodoCommand GenerateValidCommand()
    {
        return deleteTodoHandlerFaker.Generate();
    }
}
