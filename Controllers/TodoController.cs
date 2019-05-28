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
            var todoItem = await _todoContext.TodoItems.FindAsync(id);
            if(todoItem == null) {
                return NotFound();
            }
            return todoItem;
        }

        /** POST api/Todo
         * save a new TodoItem
         */
        [HttpPost]
        public async Task<ActionResult<TodoItem>> SaveTodoItem(TodoItem item) {
            _todoContext.TodoItems.Add(item);
            await _todoContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItem), new { id = item.id }, item);
        }

        /** PUT api/Todo/{id}
         * updates a TodoItem
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItem item){
            if(id != item.id){
                return BadRequest();
            }
            _todoContext.Entry(item).State = EntityState.Modified;
            await _todoContext.SaveChangesAsync();
            return NoContent();
        }

        /** DELETE api/Todo/{id}
         * deletes a TodoItem
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id) {
            var todoItem = await _todoContext.TodoItems.FindAsync(id);
            if(todoItem == null){
                return NotFound();
            }
            _todoContext.TodoItems.Remove(todoItem);
            await _todoContext.SaveChangesAsync();
            return NoContent();
        }
    }
}