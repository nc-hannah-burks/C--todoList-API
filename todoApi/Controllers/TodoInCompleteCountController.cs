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
        public async Task<TodoIncompleteCount> GetTodoincompleteCount()
        {
            List<TodoItem> todoItems = await _context.TodoItems.ToListAsync();
            IEnumerable<TodoItem> todoItems1 = todoItems.Where(toDo => toDo.IsComplete == false);
            Console.WriteLine("Hannah testing");
            int incompleteCount = todoItems1.Count();


            // Create an instance of TodoCompleteCount and set the CompleteCount property
            var result = new TodoIncompleteCount
            {
                // Assuming you want to set a static ID or generate it as needed
                incompleteCount = incompleteCount
            };

            return result;

        }

    }
}