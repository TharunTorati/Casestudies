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
    public class AnonymousUserController : ControllerBase
    {
        private readonly IMenuItemOperation<MenuItem> repo;
        public AnonymousUserController(IMenuItemOperation<MenuItem> repo)
        {
            this.repo = repo;
        }
        public IActionResult Get()
        {
            var menu = repo.GetMenuItems();
            return Ok(menu);
        }
    }
}
