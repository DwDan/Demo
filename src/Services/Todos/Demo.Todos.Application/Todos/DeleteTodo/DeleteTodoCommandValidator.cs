using FluentValidation;

namespace Demo.Todos.Application.Todos.DeleteTodo;

public class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteTodoCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Todo ID is required");
    }
}
