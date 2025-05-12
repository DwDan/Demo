using Demo.Todos.Application.Todos.GetTodo;
using FluentAssertions;

namespace Demo.Todos.Tests.Unit.Validators;

public class GetTodoCommandValidatorTests
{
    [Fact(DisplayName = "Given invalid todo data When getting todo Then throws validation exception")]
    public void Handle_InvalidRequest_ThrowsValidationException()
    {
        var validator = new GetTodoCommandValidator();
        var command = new GetTodoCommand(0);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Id");
    }
}