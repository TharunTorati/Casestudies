﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TruYumWebApiCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ClaimsIdentity userRole;

        private string GenerateJSONWebToken(string userRole)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysuperdupersecret"));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,userRole),
            };
            /*if (userId == -1)
            {
                claims = new List<Claim>
                {
                    new Claim("UserId", userId.ToString())
                };
            }
            else if (userId == 1)
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("UserId", userId.ToString())
                };
            }
            else
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Customer"),
                    new Claim("UserId", userId.ToString())
                };
            }*/

            JwtSecurityToken token = new JwtSecurityToken(
                        issuer: "mySystem",
                        audience: "myUsers",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == -1)
            {
                var token = GenerateJSONWebToken("");
                return Ok(token);
            }
            else if (id == 1)
            {
                var token = GenerateJSONWebToken("Admin");
                return Ok(token);
            }
            else
            {
                var token = GenerateJSONWebToken("Customer");
                return Ok(token);
            }


        }

        
    }
}
