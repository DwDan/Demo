using FluentValidation;

namespace Demo.Todos.Application.Todos.CreateTodo;

public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(todo => todo.Title).NotEmpty();
        RuleFor(todo => todo.Description).NotEmpty();
        RuleFor(todo => todo.DueDate).NotEmpty();
    }
}