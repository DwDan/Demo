using Demo.Todos.Application.Todos.UpdateTodo;
using FluentAssertions;

namespace Demo.Todos.Tests.Unit.Validators;

public class UpdateTodoCommandValidatorTests
{
    [Fact(DisplayName = "Given invalid todo data When updating todo Then throws validation exception")]
    public void Handle_InvalidRequest_ThrowsValidationException()
    {
        var validator = new UpdateTodoCommandValidator();
        var command = new UpdateTodoCommand();

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Id");
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Title");
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Description");
        result.Errors.Should().ContainSingle(e => e.PropertyName == "DueDate");
    }
}
