using FluentValidation;

namespace Demo.Todos.Application.Todos.DeleteTodo;

public class DeleteTodoValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteTodoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Todo ID is required");
    }
}
