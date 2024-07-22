using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoUsersController : ControllerBase
{
    private readonly TodoContext _context;

    public TodoUsersController(TodoContext context)
    {
        _context = context;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoUser>>> GetTodoUsers()
    {
        return await _context.TodoUsers.ToListAsync();
    }

    // GET: api/TodoUsers/5
    // <snippet_GetByID>
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoUser>> GetTodoUser(int id)
    {
        var todoUser = await _context.TodoUsers.FindAsync(id);

        if (todoUser == null)
        {
            return NotFound();
        }

        return todoUser;
    }
    // </snippet_GetByID>

    // PUT: api/TodoUsers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Update>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, TodoUser updatedUser)
    {
        if (id != updatedUser.UserId)
        {
            return BadRequest("Id in the route parameter does not match Id in the request body");
        }

        var todoUser = await _context.TodoUsers.FindAsync(id);
        if (todoUser == null)
        {
            return NotFound();
        }

        todoUser.UserName = updatedUser.UserName;
        todoUser.UserEmail = updatedUser.UserEmail;
        todoUser.UserPassword = updatedUser.UserPassword;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!TodoUserExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }
    // </snippet_Update>

    // POST: api/TodoUsers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Create>
    [HttpPost]
    public async Task<ActionResult<TodoUser>> PostTodoUser(TodoUser todoUser)
    {
        var newTodoUser = new TodoUser()
        { UserName = todoUser.UserName, ConfirmPassword = todoUser.ConfirmPassword, UserEmail = todoUser.UserEmail, UserPassword = todoUser.ConfirmPassword };


        _context.TodoUsers.Add(newTodoUser);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTodoUser),
            new { id = todoUser.UserId }, newTodoUser);
    }
    // </snippet_Create>

    // DELETE: api/TodoUsers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoUser(int id)
    {
        var deletedTodoUser = await _context.TodoUsers.FindAsync(id);
        if (deletedTodoUser == null)
        {
            return NotFound();
        }

        _context.TodoUsers.Remove(deletedTodoUser);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoUserExists(int id)
    {
        return _context.TodoUsers.Any(e => e.UserId == id);
    }


}