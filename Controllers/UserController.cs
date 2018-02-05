using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System;
using System.Linq;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }


         IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            User[] users = GetAll().ToArray();

            if (Array.Find(users, user)) {
                    
            }


            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = user.Id }, user);
        }
    }
}