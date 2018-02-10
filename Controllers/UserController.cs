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

        // Create New User and return its JWT token
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                // TODO: handle db errors
                Console.WriteLine(e);
                return StatusCode(500);
            }

            var token = new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Create())
                                .AddIssuer("ToDo API")
                                .AddClaim("login", user.login)
                                .AddClaim("userID", user.id.ToString())
                                .Build();

            return Ok(token.Value);
        }
    }
}