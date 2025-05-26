
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }
    
    // Changed from "Task" to "TaskItem"
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}