﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Renovation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
         public IActionResult GetAllUsers()
        {
            string[] users = new string[] { "1User", "2User" };
            return Ok(users);
        }
    }
}
