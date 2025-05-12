using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Shared;

namespace api.Interfaces
{
    public interface IAuthService
    {
        Task<Result<LoginDto>> LoginAsync(LoginRequestDto loginRequestDto);
        Task<Result<AppUserDto>> RegisterAsync(RegisterRequestDto registerRequestDto);

    }
}