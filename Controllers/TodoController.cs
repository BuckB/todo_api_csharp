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
    }
}