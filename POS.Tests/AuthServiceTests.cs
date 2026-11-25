using Moq;
using POS.Server.Repositories;
using POS.Server.Services;
using POS.Shared.DTOs;
using POS.Shared.Entities;
using Xunit;

namespace POS.Tests
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task RegisterAsync_ShouldReturnNull_WhenUserAlreadyExists()
        {
            // 1. ARRANGE (Preparar el escenario)
            var mockRepo = new Mock<IUserRepository>();
            var mockConfig = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();

            // Simulamos que la base de datos dice que el usuario YA EXISTE
            mockRepo.Setup(repo => repo.UserExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var authService = new AuthService(mockRepo.Object, mockConfig.Object);
            var request = new RegisterRequest { Email = "test@test.com", Password = "123" };

            // 2. ACT (Ejecutar el método)
            var result = await authService.RegisterAsync(request);

            // 3. ASSERT (Verificar el resultado)
            Assert.Null(result); // El resultado debe ser nulo porque el usuario ya existe
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenPasswordIsIncorrect()
        {
            // ARRANGE
            var mockRepo = new Mock<IUserRepository>();
            var mockConfig = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();

            // Simulamos un usuario en la BD con una contraseña encriptada real
            var user = new User
            {
                Email = "test@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("PasswordCorrecto")
            };
            mockRepo.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

            var authService = new AuthService(mockRepo.Object, mockConfig.Object);
            var request = new LoginRequest { Email = "test@test.com", Password = "PasswordIncorrecto" };

            // ACT
            var result = await authService.LoginAsync(request);

            // ASSERT
            Assert.Null(result); // No debe generar token
        }
    }
}