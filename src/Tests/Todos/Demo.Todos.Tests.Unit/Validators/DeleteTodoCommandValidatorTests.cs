using Demo.Todos.Application.Todos.DeleteTodo;
using FluentAssertions;

namespace Demo.Todos.Tests.Unit.Validators;

public class DeleteTodoCommandValidatorTests
{
    [Fact(DisplayName = "Given invalid todo data When deleting todo Then throws validation exception")]
    public void Handle_InvalidRequest_ThrowsValidationException()
    {
        var validator = new DeleteTodoCommandValidator();
        var command = new DeleteTodoCommand(0);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Id");
    }
}