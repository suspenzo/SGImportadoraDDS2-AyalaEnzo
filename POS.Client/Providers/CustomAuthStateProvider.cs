using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace POS.Client.Providers
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;
        private readonly string _tokenKey = "authToken";

        public CustomAuthStateProvider(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // 1. Buscar el token en el LocalStorage del navegador
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", _tokenKey);

            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            // 2. Decodificar el token y crear el "Usuario" para Blazor
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var identity = new ClaimsIdentity(jwt.Claims, "jwt", ClaimTypes.Name, "role");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        // Método para notificar a Blazor que el usuario se ha logueado
        public async Task MarkUserAsAuthenticated(string token)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", _tokenKey, token);
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var identity = new ClaimsIdentity(jwt.Claims, "jwt", ClaimTypes.Name, "role");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        // Método para notificar a Blazor que el usuario se ha deslogueado
        public async Task MarkUserAsLoggedOut()
        {
            try
            {
                // 👇 Avisamos al backend para que registre la hora de salida 👇
                await _httpClient.PostAsync("api/Auth/logout", null);
            }
            catch { /* Ignorar errores de red al cerrar sesión */ }

            // Eliminamos el token del almacenamiento del navegador
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", _tokenKey);

            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }
    }
}