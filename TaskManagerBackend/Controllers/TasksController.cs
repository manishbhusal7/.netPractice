
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;


namespace TaskManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskDbContext _context;

    public TasksController(TaskDbContext context)
    {
        _context = context;
    }

    // GET: api/tasks
    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> GetTasks()
    {
        return _context.Tasks.ToList();
    }

    // GET: api/tasks/5 (get task by ID)
    [HttpGet("{id}")]
    public ActionResult<TaskItem> GetTask(int id)
    {
        var task = _context.Tasks.Find(id);

        if (task == null)
        {
            return NotFound(); // 404 Not Found
        }

        return task;
    }

    // POST: api/tasks
    [HttpPost]
    public ActionResult<TaskItem> CreateTask(TaskItem task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    // PUT: api/tasks/5 (update entire task)
    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, TaskItem task)
    {
        if (id != task.Id)
        {
            return BadRequest(); // 400 Bad Request
        }

        _context.Entry(task).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent(); // 204 No Content
    }

    // DELETE: api/tasks/5
    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var task = _context.Tasks.Find(id);

        if (task == null)
        {
            return NotFound(); // 404 Not Found
        }

        _context.Tasks.Remove(task);
        _context.SaveChanges();

        return NoContent(); // 204 No Content
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> MarkTaskCompleted(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();

        task.IsCompleted = true;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}