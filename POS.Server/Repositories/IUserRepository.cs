using POS.Shared.Entities;

namespace POS.Server.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
    }
}