using Demo.Todos.Domain.Enums;

namespace Demo.Todos.WebApi.Features.Todos.Common
{
    public class TodoPresentation
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TodoStatus Status { get; set; } = TodoStatus.Pending;

        public DateTime DueDate { get; set; }
    }
}
