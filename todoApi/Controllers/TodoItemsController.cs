using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly TodoContext _context;

    public TodoItemsController(TodoContext context)
    {
        _context = context;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems(int todoUserId)
    {
        var todoItems = await _context.TodoItems
        .Where(x => x.UserId == todoUserId)
        .ToListAsync();



        return Ok(todoItems);
    }

    // GET: api/TodoItems/5
    // <snippet_GetByID>
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodoItem(long id, int todoUserId)
    {
        var todoItem = await _context.TodoItems
               .Where(t => t.Id == id && t.UserId == todoUserId)
               .FirstOrDefaultAsync();

        // Check if the item exists
        if (todoItem == null)
        {
            return NotFound(); // Return 404 Not Fsound if the item does not exist
        }

        // Return the found item with 200 OK status
        return Ok(todoItem);
    }

    // </snippet_GetByID>

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Update>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoDTO)
    {
        if (id != todoDTO.Id)
        {
            return BadRequest();
        }

        var todoItem = await _context.TodoItems.FindAsync(id);

        // Check if the fetched todoItem exists and belongs to the specified UserId
        if (todoItem == null || todoItem.UserId != todoDTO.UserId)
        {
            return NotFound("Post not possible -  TodoItem not found or does not belong to the specified user");
        }

        todoItem.Name = todoDTO.Name;
        todoItem.IsComplete = todoDTO.IsComplete;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }
    // </snippet_Update>

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Create>
    [HttpPost]
    public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemPost toDoItemPost)
    {
        try
        {
            var user = await _context.TodoUsers.FindAsync(toDoItemPost.UserId);

            if (user == null)
            {
                return NotFound($"User with ID {toDoItemPost.UserId} not found.");
            }

            var todoItem = new TodoItem
            {
                IsComplete = toDoItemPost.IsComplete,
                Name = toDoItemPost.Name,
                Details = toDoItemPost.Details,
                UserId = toDoItemPost.UserId,
                TodoUser = user
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }
        catch (Exception ex)
        {
            // Log the exception (ex) for further analysis
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    // </snippet_Create>

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id, int userId)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        if (todoItem.UserId != userId)
        {
            return NotFound("Delete not possible - TodoItem not found or does not belong to the specified user");
        }

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoItemExists(long id)
    {
        return _context.TodoItems.Any(e => e.Id == id);
    }

    private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
       new TodoItemDTO
       {
           Id = todoItem.Id,
           Name = todoItem.Name,
           IsComplete = todoItem.IsComplete,
           Details = todoItem.Details,
           UserId = todoItem.UserId,
           TodoUser = todoItem.TodoUser

       };
};