using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TruYumWebApiCaseStudy.Models;

namespace TruYumWebApiCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IMenuItemOperation<MenuItem> repo;
        private readonly IMenuItemOperation<Cart> cartRepo;
        public CustomerController(IMenuItemOperation<MenuItem> repo, IMenuItemOperation<Cart> cartRepo)
        {
            this.repo = repo;
            this.cartRepo = cartRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var menuItems = repo.GetMenuItems(m => m.Active == true && m.LaunchDate <= DateTime.Now);
            if (menuItems == null)
                return NoContent();
            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cartDetails = cartRepo.GetMenuItems(u => u.UserId == id);
            if (cartDetails == null)
                return NoContent();
            int totalPrice = 0;
            for (int i = 0; i < cartDetails.Count; i++)
            {
                var menuItem = repo.GetMenuItems(u => u.Id == cartDetails[i].MenuItemId).FirstOrDefault();
                totalPrice += menuItem.Price;
            }
            return Ok(new { cartDetails, totalPrice });
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cart cart)
        {
            cartRepo.Add(cart);
            int rowsAffected =await cartRepo.SaveAsync();
            if (rowsAffected == 1)
                return Ok("Cart item added");
            return BadRequest("Failed");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id,int menuItemId)
        {
            var cartItem = cartRepo.GetMenuItems(u => u.UserId == id && u.MenuItemId == menuItemId).FirstOrDefault();
            cartRepo.Delete(cartItem);
            int rowsAffected =await cartRepo.SaveAsync();
            if (rowsAffected == 1)
                return Ok("Item deleted");
            return BadRequest("failed");
        }
    }
}
