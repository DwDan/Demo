using FluentValidation;

namespace Demo.Todos.WebApi.Features.Todos.GetTodo;

public class GetTodoRequestValidator : AbstractValidator<GetTodoRequest>
{
    public GetTodoRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Todo ID is required");
    }
}