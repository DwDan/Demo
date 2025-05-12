using Demo.Todos.Domain.Entities;
using Demo.Todos.Domain.Enums;
using FluentAssertions;

namespace Demo.Todos.Tests.Unit.Entities;

public class TodoTests
{
    [Fact(DisplayName = "Should create a valid todo")]
    public void CreateTodo_ValidData_ShouldSucceed()
    {
        // Arrange
        var title = "Buy bread";
        var description = "Go to the bakery before 8 AM";
        var dueDate = DateTime.UtcNow.AddDays(1);

        // Act
        var todo = new Todo(title, description, dueDate);

        // Assert
        todo.Title.Should().Be(title);
        todo.Description.Should().Be(description);
        todo.DueDate.Should().Be(dueDate);
        todo.Status.Should().Be(TodoStatus.Pending);
    }

    [Theory(DisplayName = "Should throw exception if title is null or empty")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void SetTitle_InvalidValue_ShouldThrow(string? invalidTitle)
    {
        var todo = new Todo("Title", "Description", DateTime.UtcNow.AddDays(1));
        var act = () => todo.SetTitle(invalidTitle!);
        act.Should().Throw<ArgumentException>().WithMessage("*Title*");
    }

    [Theory(DisplayName = "Should throw exception if description is null or empty")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void SetDescription_InvalidValue_ShouldThrow(string? invalidDescription)
    {
        var todo = new Todo("Title", "Description", DateTime.UtcNow.AddDays(1));
        var act = () => todo.SetDescription(invalidDescription!);
        act.Should().Throw<ArgumentException>().WithMessage("*Description*");
    }

    [Fact(DisplayName = "Should throw exception if due date is in the past")]
    public void SetDueDate_PastDate_ShouldThrow()
    {
        var todo = new Todo("Title", "Description", DateTime.UtcNow.AddDays(1));
        var act = () => todo.SetDueDate(DateTime.UtcNow.AddDays(-1));
        act.Should().Throw<ArgumentException>().WithMessage("*Due date*");
    }

    [Fact(DisplayName = "Should mark todo as completed")]
    public void MarkAsCompleted_ShouldUpdateStatus()
    {
        var todo = new Todo("Title", "Description", DateTime.UtcNow.AddDays(1));
        todo.MarkAsCompleted();
        todo.Status.Should().Be(TodoStatus.Completed);
    }

    [Fact(DisplayName = "Should reopen a todo")]
    public void Reopen_ShouldSetStatusToPending()
    {
        var todo = new Todo("Title", "Description", DateTime.UtcNow.AddDays(1));
        todo.MarkAsCompleted();
        todo.Reopen();
        todo.Status.Should().Be(TodoStatus.Pending);
    }

    [Fact(DisplayName = "Should allow usage of internal methods in tests")]
    public void SetIdAndStatusForTests_ShouldWork()
    {
        var todo = new Todo("Title", "Description", DateTime.UtcNow.AddDays(1));
        todo.SetIdForTests(99);
        todo.SetStatusForTests(TodoStatus.Completed);

        todo.Id.Should().Be(99);
        todo.Status.Should().Be(TodoStatus.Completed);
    }
}
