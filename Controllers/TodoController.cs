using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace TodoApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }


        public IEnumerable<TodoItem> GetAll()
        {
            int userId = getUserIdFromJwtToken();
            return _context.TodoItems.Where(t => t.AuthorId == userId).ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            int userId = getUserIdFromJwtToken();
            var item = getUserItemById(id);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            int userID = getUserIdFromJwtToken();
            item.AuthorId = userID;

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = getUserItemById(id);

            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = getUserItemById(id);

            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();

            return new NoContentResult();
        }

        private int getUserIdFromJwtToken()
        {
            int userID;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int.TryParse(identity.FindFirst("userID").Value, out userID);
            return userID;
        }

        private TodoItem getUserItemById(long itemId)
        {
            int userId = getUserIdFromJwtToken();
            return _context.TodoItems.FirstOrDefault(t => (t.Id == itemId && t.AuthorId == userId));
        }

    }
}