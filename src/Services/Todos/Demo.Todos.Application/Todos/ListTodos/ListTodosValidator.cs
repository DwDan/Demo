using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Validation;
using FluentValidation;

namespace Demo.Todos.Application.Todos.ListTodos;

public class ListTodosValidator : AbstractValidator<ListTodosCommand>
{
    public ListTodosValidator()
    {
        RuleFor(x => x.Order).SetValidator(new OrderValidator<Todo>());
        RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page must be greater than 0");
        RuleFor(x => x.Size).GreaterThan(0).WithMessage("Page size must be greater than 0");
    }
}
