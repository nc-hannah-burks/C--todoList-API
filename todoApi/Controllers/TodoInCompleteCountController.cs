using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;


namespace todoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoIncompleteCountController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoIncompleteCountController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/toDoInCompleteCount
        [HttpGet]
        public async Task<TodoIncompleteCount> GetTodoincompleteCount(int todoUserId)
        {
            List<TodoItem> todoItems = await _context.TodoItems.ToListAsync();
            IEnumerable<TodoItem> usersInCompletedTodoItems = todoItems.Where(toDo => toDo.IsComplete == false).Where(x => x.UserId == todoUserId); ;

            int incompleteCount = usersInCompletedTodoItems.Count();


            // Create an instance of TodoCompleteCount and set the CompleteCount property
            var result = new TodoIncompleteCount
            {
                UserId = todoUserId,
                // Assuming you want to set a static ID or generate it as needed
                incompleteCount = incompleteCount
            };

            return result;

        }

    }
}
