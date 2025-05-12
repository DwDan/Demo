using Demo.Todos.Domain.Enums;

namespace Demo.Todos.Domain.Entities;

public class Todo
{
    public int Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty; 

    public TodoStatus Status { get; private set; }

    public DateTime DueDate { get; private set; }

    protected Todo() { }

    public Todo(string title, string description, DateTime dueDate)
    {
        SetTitle(title);
        SetDescription(description);
        SetDueDate(dueDate);
        Status = TodoStatus.Pending;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");

        Title = title.Trim();
    }

    public void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.");

        Description = description.Trim();
    }

    public void SetDueDate(DateTime dueDate)
    {
        if (dueDate < DateTime.UtcNow.Date)
            throw new ArgumentException("Due date cannot be in the past.");

        DueDate = dueDate;
    }

    public void MarkAsCompleted()
    {
        Status = TodoStatus.Completed;
    }

    public void Reopen()
    {
        Status = TodoStatus.Pending;
    }

    internal void SetStatusForTests(TodoStatus status)
    {
        Status = status;
    } 
    
    internal void SetIdForTests(int id)
    {
        Id = id;
    }
}
