using FluentValidation;

namespace Demo.Todos.Application.Todos.GetTodo;

public class GetTodoValidator : AbstractValidator<GetTodoCommand>
{
    public GetTodoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Todo ID is required");
    }
}
