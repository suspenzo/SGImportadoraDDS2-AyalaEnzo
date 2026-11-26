using POS.Shared.DTOs;

namespace POS.Server.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);

        Task<List<UserResponse>> GetAllUsersAsync();

        Task<UserResponse?> GetUserByIdAsync(int id);

        Task<UserResponse?> UpdateUserAsync(int id, UserRequest request);
        Task<bool> DeleteUserAsync(int id);
        Task LogoutAsync(int userId);
        Task<List<LoginHistoryDto>> GetLoginHistoryAsync();
    }
}