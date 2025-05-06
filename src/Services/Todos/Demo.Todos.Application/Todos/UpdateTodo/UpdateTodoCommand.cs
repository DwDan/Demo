using Demo.Common.Application.Validation;
using Demo.Todos.Application.Todos.Common;
using MediatR;

namespace Demo.Todos.Application.Todos.UpdateTodo;

public class UpdateTodoCommand : TodoApplication, IRequest<UpdateTodoResult>
{
    public ValidationResultDetail Validate()
    {
        var validator = new UpdateTodoCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}