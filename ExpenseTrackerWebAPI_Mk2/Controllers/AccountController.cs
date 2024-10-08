﻿using ExpenseTrackerWebAPI_Mk2.Interfaces;
using ExpenseTrackerWebAPI_Mk2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTrackerWebAPI_Mk2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _userRepository = userRepository;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser model)
        {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var userCreated = await _userManager.FindByNameAsync(model.Username);
                if (userCreated != null)
                {
                    result = await _userManager.AddToRoleAsync(userCreated, "User");
                    if(result.Succeeded)
                    {
                        //Adding details of new user created to the user table
                        Models.User newUser = new User { UserID = new Guid(userCreated.Id), FirstName = model.FirstName, LastName = model.LastName, Status = true };
                        _userRepository.CreateUser(newUser);

                        return Ok(new { message = "User Registered and assigned role" });
                    }
                }
            }

            return BadRequest(result.Errors);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName ),
                    new Claim("userID", user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() ),

                };

                authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return Unauthorized();
        }

        //[HttpPost("add-role")]
        //public async Task<IActionResult> AddRole([FromBody] string role)
        //{
        //    if (!await _roleManager.RoleExistsAsync(role))
        //    {
        //        var result = await _roleManager.CreateAsync(new IdentityRole(role));
        //        if (result.Succeeded)
        //        {
        //            return Ok(new { message = "Role Added Successfully" });
        //        }
        //        return BadRequest(result.Errors);
        //    }
        //    return BadRequest("Role Exists");
        //}

        //[HttpPost("assign-role")]
        //public async Task<IActionResult> AssignRole([FromBody] UserRole model)
        //{
        //    var user = await _userManager.FindByNameAsync(model.Username);
        //    if (user == null)
        //    {
        //        return BadRequest("User not found");
        //    }

        //    var result = await _userManager.AddToRoleAsync(user, model.Role);

        //    if (result.Succeeded)
        //    {
        //        return Ok(new { message = "Role Assigned" });
        //    }
        //    return BadRequest(result.Errors);
        //}
    }
}
