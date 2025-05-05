using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Service
{
    public class AuthService : IAuthService
    {   
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthService(UserManager<AppUser> userManager ,ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public async Task<LoginDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
 
            var user = await _userManager.Users.FirstOrDefaultAsync(x=> x.Email == loginRequestDto.Email.ToLower());
            if(user == null)throw new UnauthorizedAccessException("Username not found and or password incorrect!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, false);
            if(!result.Succeeded)throw new UnauthorizedAccessException("Username not found and or password incorrect!");

            var token = _tokenService.CreateToken(user);

            return user.ToLoginDto(token);

        }
        public async Task<AppUserDto> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var appUser = new AppUser
                {
                    Name = registerRequestDto.Username,
                    Email = registerRequestDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerRequestDto.Password);

                if(!createdUser.Succeeded)
                    throw new SystemException("User was not created");
                
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if(!roleResult.Succeeded)
                    throw new SystemException("User roles not set");
                
                return appUser.ToAppUserDto();
                
        }
    }
}