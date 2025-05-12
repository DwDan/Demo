using Demo.Todos.Application.Todos.ListTodos;
using FluentAssertions;

namespace Demo.Todos.Tests.Unit.Validators;

public class ListTodosCommandValidatorTests
{
    [Fact(DisplayName = "Given invalid todo data When listing todo Then throws validation exception")]
    public void Handle_InvalidRequest_ThrowsValidationException()
    {
        var validator = new ListTodosCommandValidator();
        var command = new ListTodosCommand() { Size = 0, Page = 0, Order = "test"};

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Order");
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Page");
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Size");
    }
}