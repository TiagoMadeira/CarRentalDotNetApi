using api.Models;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser?> GetByEmailAsync(string email);

        Task<bool> UserExistsAsync(string email);

    }
}
