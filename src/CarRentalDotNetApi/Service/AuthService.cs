using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<Result<LoginDto>> LoginAsync(LoginRequestDto loginRequestDto)
        {
 
            var user = await _userManager.Users.FirstOrDefaultAsync(x=> x.Email == loginRequestDto.Email.ToLower());
            if (user == null) return Result<LoginDto>.Failure(AuthErrors.LoginError);
               

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, false);
            if(!result.Succeeded) return Result<LoginDto>.Failure(AuthErrors.LoginError);

            var token = _tokenService.CreateToken(user);

            return  Result<LoginDto>.Success(user.ToLoginDto(token));

        }
        public async Task<Result<AppUserDto>> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var appUser = new AppUser
                {
                    Name = registerRequestDto.Username,
                    Email = registerRequestDto.Email,
                };

            var createdUser = await _userManager.CreateAsync(appUser, registerRequestDto.Password);

            if(!createdUser.Succeeded) return Result<AppUserDto>.Failure(AuthErrors.RegisterError);

            var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if(!roleResult.Succeeded) return Result<AppUserDto>.Failure(AuthErrors.RegisterError);


            return Result<AppUserDto>.Success( appUser.ToAppUserDto());
                
        }
    }
}