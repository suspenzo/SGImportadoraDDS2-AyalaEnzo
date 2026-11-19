using Microsoft.IdentityModel.Tokens;
using POS.Server.Repositories;
using POS.Shared.DTOs;
using POS.Shared.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;

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
            // 1. Verificar si el usuario ya existe
            if (await _userRepository.UserExistsAsync(request.Email))
            {
                return null; // Usuario ya existe
            }

            // 2. Encriptar la contraseña 
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // 3. Crear el objeto User
            var newUser = new User
            {
                Name = request.FullName,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = request.Role
            };

            // 4. Guardar en la Base de Datos usando el Repositorio
            var createdUser = await _userRepository.CreateUserAsync(newUser);

            // 5. Generar la respuesta con el Token (Imagen 5)
            return GenerateAuthResponse(createdUser);
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            // 1. Buscar al usuario por Email
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null) return null;

            // 2. Verificar la contraseña (Imagen 6)
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return null; // Contraseña incorrecta
            }

            // 3. Generar la respuesta con el Token
            return GenerateAuthResponse(user);
        }

        // --- MÉTODOS PRIVADOS PARA GENERAR TOKEN Y RESPUESTA (Imágenes 2, 3 y 10) ---

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
                    SecurityAlgorithms.HmacSha256Signature) // Asegúrate de tener el using de esto
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            // Convertimos la lista de entidades a lista de DTOs para ocultar el hash
            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role.ToString()
            }).ToList();
        }
    }
}