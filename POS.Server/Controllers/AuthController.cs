using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Server.Services;
using POS.Shared.DTOs;
using System.Security.Claims;

namespace POS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // Inyección del servicio
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
        {
            var response = await _authService.RegisterAsync(request);

            if (response == null)
            {
                return BadRequest("Usuario ya existe");
            }

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);

            if (response == null)
            {
                return Unauthorized("Credenciales invalidas");
            }

            return Ok(response);
        }

        [HttpGet("usuarios")]
        [Authorize(Roles = "Administrador")] //  Solo los usuarios con rol Administrador pueden entrar
        public async Task<ActionResult<List<UserRequest>>> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<UserResponse>> UpdateUser(int id, UserRequest request)
        {
            var updated = await _authService.UpdateUserAsync(id, request);
            if (updated == null) return NotFound("Usuario no encontrado");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _authService.DeleteUserAsync(id);
            if (!deleted) return NotFound("Usuario no encontrado");
            return NoContent();
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim != null)
            {
                await _authService.LogoutAsync(int.Parse(userIdClaim));
            }
            return Ok();
        }

        [HttpGet("historial")]
        [Authorize(Roles = "Administrador")] // Solo el admin puede ver esto
        public async Task<ActionResult<List<LoginHistoryDto>>> GetHistorial()
        {
            return Ok(await _authService.GetLoginHistoryAsync());
        }
    }
}