using POS.Shared.Entities;

namespace POS.Server.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
    }
}