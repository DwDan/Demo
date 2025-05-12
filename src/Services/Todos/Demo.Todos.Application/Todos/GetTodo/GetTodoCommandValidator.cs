using FluentValidation;

namespace Demo.Todos.Application.Todos.GetTodo;

public class GetTodoCommandValidator : AbstractValidator<GetTodoCommand>
{
    public GetTodoCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Todo ID is required");
    }
}
