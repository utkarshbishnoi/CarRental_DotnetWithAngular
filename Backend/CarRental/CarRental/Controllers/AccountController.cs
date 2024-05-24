using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DAL.Entities;
using BLL.Facade;
using BLL.Services;
using DAL.Repository;
using CarRental.DTO;

namespace CarRental.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IFacadeOperations _userFacade;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHashService _passwordHashService;

        public AccountController(IFacadeOperations _userFacade, ICarOperations carOperations, ITokenService tokenService, IPasswordHashService passwordHashService)
        {
            this._userFacade = _userFacade;
            this._tokenService = tokenService;
            this._passwordHashService = passwordHashService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Authenticate the user
            var user = await _userFacade.GetUserByEmail(userLogin.Email);
            if (user == null || !_passwordHashService.VerifyPassword(userLogin.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password");
            }

            // Generate the token
            var token = _tokenService.GenerateToken(user.Id, user.Role);


            // Create a response DTO with the token and role information
            var response = new
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Token = token
            };

            return Ok(response);
        }


    }
}
