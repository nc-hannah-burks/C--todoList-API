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
    public class TodoCompleteCountController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoCompleteCountController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/toDoCompleteCount
        [HttpGet]
        public async Task<TodoCompleteCount> GetTodoCompleteCount(int todoUserId)
        {
            List<TodoItem> todoItems = await _context.TodoItems.ToListAsync();
            IEnumerable<TodoItem> usersCompletedTodoItems = todoItems.Where(toDo => toDo.IsComplete == true).Where(x => x.UserId == todoUserId);

            int completeCount = usersCompletedTodoItems.Count();


            // Create an instance of TodoCompleteCount and set the CompleteCount property
            var result = new TodoCompleteCount

            {

                UserId = todoUserId, //setting userId to given one
                // Assuming you want to set a static ID or generate it as needed
                CompleteCount = completeCount
            };


            return result;

        }

    }
}
