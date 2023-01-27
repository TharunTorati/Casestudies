using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruYumWebApiCaseStudy.Models;

namespace TruYumWebApiCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMenuItemOperation<User> repo;
        public UserController(IMenuItemOperation<User> repo)
        {
            this.repo = repo;
        }

        [HttpPost]

        public async Task<IActionResult> Post([FromBody] User user)
        {
            repo.Add(user);
            int rowsAffected = await repo.SaveAsync();
            if (rowsAffected == 1)
                return Ok("User Information Added to database");
            return BadRequest("Failed");
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id,[FromBody]string password)
        {
            var userDetails = repo.GetMenuItems(u => u.Id == id).FirstOrDefault();
            if (userDetails == null)
                return BadRequest("User does not exist");
            if (userDetails.Password == password)
                return Ok("Login successful");
            return BadRequest("UserId/Password is incorrect");
        }
    }
}
