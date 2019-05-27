using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase {
        private readonly TodoContext _todoContext;
        public TodoController(TodoContext todoContext) {
            _todoContext = todoContext;
            if(_todoContext.TodoItems.Count() == 0) {
                //create a new Todo item if collection is empty
                _todoContext.TodoItems.Add(new TodoItem{ name = "Add a new item!", isComplete = false });
                _todoContext.SaveChanges();
            }
        }

        /** GET api/Todo
         * returns all Todos
         */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAllTodos() {
            return await _todoContext.TodoItems.ToListAsync();
        }

        /** GET api/Todo/{id}
         * returns a specific Todo item
         */
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id) {
            var TodoItem = await _todoContext.TodoItems.FindAsync(id);
            if(TodoItem == null) {
                return NotFound();
            }
            return TodoItem;
        }
    }
}