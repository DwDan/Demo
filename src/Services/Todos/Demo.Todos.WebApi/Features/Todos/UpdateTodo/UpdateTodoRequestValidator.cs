using FluentValidation;

namespace Demo.Todos.WebApi.Features.Todos.UpdateTodo;

public class UpdateTodoRequestValidator : AbstractValidator<UpdateTodoRequest>
{
    public UpdateTodoRequestValidator()
    {
        RuleFor(todo => todo.Id).NotEmpty();
        RuleFor(todo => todo.Name).NotEmpty();
    }
}