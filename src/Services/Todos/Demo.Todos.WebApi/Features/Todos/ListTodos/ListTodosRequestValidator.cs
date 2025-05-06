using Demo.Common.WebApi;
using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Validation;
using FluentValidation;

namespace Demo.Todos.WebApi.Features.Todos.ListTodos;

public class ListTodosRequestValidator : AbstractValidator<ApiQueryRequestPresentation>
{
    public ListTodosRequestValidator()
    {
        RuleFor(x => x.Order).SetValidator(new OrderValidator<Todo>());
        RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page must be greater than 0");
        RuleFor(x => x.Size).GreaterThan(0).WithMessage("Page size must be greater than 0");
    }
}