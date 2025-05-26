namespace TaskManagerAPI.Models;

public class TaskItem
{
    // Required for Entity Framework
    public TaskItem() { }

    public TaskItem(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty; // Initialize with empty string
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}