using FluentValidation;

namespace Demo.Todos.WebApi.Features.Todos.DeleteTodo;

public class DeleteTodoRequestValidator : AbstractValidator<DeleteTodoRequest>
{
    public DeleteTodoRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Todo ID is required");
    }
}
