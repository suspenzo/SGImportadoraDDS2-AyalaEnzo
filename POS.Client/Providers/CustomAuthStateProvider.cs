using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace POS.Client.Providers
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly string _tokenKey = "authToken";

        public CustomAuthStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
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
            // 1. Eliminamos el token del almacenamiento del navegador
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", _tokenKey);

            // 2. Creamos un usuario "anónimo" (sin claims)
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

            // 3. Notificamos a Blazor que el estado cambió (esto dispara la redirección al login automáticamente por el AuthorizeRouteView)
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }
    }
}