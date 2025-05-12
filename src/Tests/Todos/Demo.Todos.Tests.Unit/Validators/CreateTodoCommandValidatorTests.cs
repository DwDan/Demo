using Demo.Todos.Application.Todos.CreateTodo;
using FluentAssertions;

namespace Demo.Todos.Tests.Unit.Validators;

public class CreateTodoCommandValidatorTests
{
    [Fact(DisplayName = "Given invalid todo data When creating todo Then throws validation exception")]
    public void Handle_InvalidRequest_ThrowsValidationException()
    {
        var validator = new CreateTodoCommandValidator();
        var command = new CreateTodoCommand();

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Title");
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Description");
        result.Errors.Should().ContainSingle(e => e.PropertyName == "DueDate");
    }
}