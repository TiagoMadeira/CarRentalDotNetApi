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
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthService(UserManager<AppUser> userManager ,ITokenService tokenService, SignInManager<AppUser> signInManager, IUserRepository userRepo)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userRepo = userRepo;
            _signInManager = signInManager;
        }

        public async Task<Result<LoginDto>> LoginAsync(LoginRequestDto loginRequestDto)
        {
 
            var user = await _userRepo.GetByEmailAsync(loginRequestDto.Email);
            if (user == null) return Result<LoginDto>.Failure(AuthErrors.LoginError);
               
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, false);
            if(!result.Succeeded) return Result<LoginDto>.Failure(AuthErrors.LoginError);

            var token = _tokenService.CreateToken(user);

            return  Result<LoginDto>.Success(user.ToLoginDto(token));

        }
        public async Task<Result<AppUserDto>> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var appUserModel = registerRequestDto.RegisterRequestDtoToAppUser();

            var result = await _userRepo.UserExistsAsync(appUserModel.Email);
            if (result) return Result<AppUserDto>.Failure(AuthErrors.EmailAlreadyExistsrError);

            var createdUser = await _userManager.CreateAsync(appUserModel, registerRequestDto.Password);
            if(!createdUser.Succeeded) return Result<AppUserDto>.Failure(AuthErrors.RegisterError);

            var roleResult = await _userManager.AddToRoleAsync(appUserModel, "User");
            if(!roleResult.Succeeded) return Result<AppUserDto>.Failure(AuthErrors.RegisterError);

            return Result<AppUserDto>.Success(appUserModel.ToAppUserDto());    
        }
    }
}