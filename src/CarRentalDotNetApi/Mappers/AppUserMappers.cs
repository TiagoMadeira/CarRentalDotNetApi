using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Models;

namespace api.Mappers
{
    public static class AppUserMappers
    {
        public static LoginDto ToLoginDto(this AppUser appUserModel, string token)
        {
        return new LoginDto
        {
            UserName = appUserModel.Name,
            Email = appUserModel.Email, 
            Token = token
        };
        } 

       public static AppUserDto ToAppUserDto(this AppUser appUserModel)
       {
        return new AppUserDto
        {
            UserName = appUserModel.Name ,
            Email = appUserModel.Email
        };
       }

        public static AppUser RegisterRequestDtoToAppUser(this RegisterRequestDto registerRequestDto)
        {
            return new AppUser
            {
                Name = registerRequestDto.Username,
                Email = registerRequestDto.Email
            };
        }
    }
}