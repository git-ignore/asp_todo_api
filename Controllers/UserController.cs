using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace TodoApi.Controllers
{   
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }


        private IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        // Create New User
        [AllowAnonymous]
        [HttpPost] 
        public IActionResult Create([FromBody] User user)
        {
            if (user == null) {
                return BadRequest();
            }

            User[] users = GetAll().ToArray();

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();

            } catch(Exception e) {
                // TODO: handle db errors
                Console.WriteLine(e);

                return StatusCode(500);
            }

            var token = new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Create("Supersecret service kay mama"))
                                .AddIssuer("ToDo API")
                                .AddClaim("login", user.login)
                                .Build();

            return Ok(token.Value);
        }
    }
}