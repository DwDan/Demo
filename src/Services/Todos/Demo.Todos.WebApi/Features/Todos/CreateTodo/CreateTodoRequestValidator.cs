using FluentValidation;

namespace Demo.Todos.WebApi.Features.Todos.CreateTodo;

public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestValidator()
    {
        RuleFor(todo => todo.Title).NotEmpty();
    }
}