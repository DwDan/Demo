using Demo.Common.Application.Validation;
using Demo.Todos.Application.Todos.Common;
using MediatR;

namespace Demo.Todos.Application.Todos.CreateTodo;

public class CreateTodoCommand : TodoApplication, IRequest<CreateTodoResult>
{
    public ValidationResultDetail Validate()
    {
        var validator = new CreateTodoCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}