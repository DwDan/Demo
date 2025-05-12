using FluentValidation;

namespace Demo.Todos.Application.Todos.UpdateTodo;

public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(todo => todo.Id).NotEmpty();
        RuleFor(todo => todo.Title).NotEmpty();
        RuleFor(todo => todo.Description).NotEmpty();
        RuleFor(todo => todo.DueDate).NotEmpty();
    }
}