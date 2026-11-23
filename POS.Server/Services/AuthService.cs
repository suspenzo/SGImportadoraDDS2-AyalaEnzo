using Microsoft.IdentityModel.Tokens;
using POS.Server.Repositories;
using POS.Shared.DTOs;
using POS.Shared.Entities;
using POS.Shared.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace POS.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
        {
            if (await _userRepository.UserExistsAsync(request.Email))
                return null;

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                Name = request.FullName, // Ojo: Asegúrate de que RegisterRequest tenga FullName
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = request.Role,
                Estado = Status.Activo,
                FechaCreacion = DateTime.Now
            };

            var createdUser = await _userRepository.CreateUserAsync(newUser);
            return GenerateAuthResponse(createdUser);
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            return GenerateAuthResponse(user);
        }

        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return MapToResponseDto(user); // Usamos un método auxiliar para no repetir código
        }

        // 👇 CORREGIDO: Ahora devuelve List<UserResponse> y no List<UserRequest>
        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(MapToResponseDto).ToList();
        }

        public async Task<UserResponse?> UpdateUserAsync(int id, UserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            // Actualizamos datos (usando 'Name' consistentemente)
            user.Name = request.Name;
            user.Email = request.Email;
            user.Role = request.Role; // Funcionará si UserRequest.Role es del tipo Enum Role
            user.Estado = request.Estado;

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            await _userRepository.UpdateAsync(user);

            return MapToResponseDto(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        // --- MÉTODOS PRIVADOS ---

        // Método auxiliar para mapear Entidad a DTO (Evita repetir código)
        private UserResponse MapToResponseDto(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString(),
                Estado = user.Estado,
                FechaCreacion = user.FechaCreacion
            };
        }

        private AuthResponse GenerateAuthResponse(User user)
        {
            var tokenString = GenerateToken(user);
            return new AuthResponse
            {
                Token = tokenString,
                UserId = user.Id,
                Email = user.Email,
                FullName = user.Name,
                Role = user.Role.ToString()
            };
        }

        private string GenerateToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpirationMinutes"]!)),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}