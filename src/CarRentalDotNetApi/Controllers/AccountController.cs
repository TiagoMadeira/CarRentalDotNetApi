using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using api.Service;
using api.Validation.InputValidators.cs;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
      
        public AccountController(IAuthService authService) {
          _authService = authService;
        }
        [HttpPost("login")]
         public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto, IValidator<LoginRequestDto> validator){

            validator.ValidateAndThrow(loginRequestDto);

            var loginDto = await _authService.LoginAsync(loginRequestDto);
            
            return Ok(loginDto);
            

         }
        [HttpPost ("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto, IValidator<RegisterRequestDto> validator){
                
            validator.ValidateAndThrow(registerDto);

            var newUserDto = await _authService.RegisterAsync(registerDto);

            return Ok(newUserDto);
                
        }
    }
}