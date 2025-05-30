

using System.ComponentModel.DataAnnotations; // Add this namespace for validation attributes

namespace TaskManagerAPI.Models;

public class TaskItem
{
    // Required for Entity Framework Core (when fetching data from the database)
    public TaskItem() { }

    // Constructor for manual object creation (optional)
    public TaskItem(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")] // Validation: Title cannot be empty
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }
}