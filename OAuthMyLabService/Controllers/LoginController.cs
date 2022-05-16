using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OAuthMyLabService.Models;
using OAuthMyLabService.Services;

namespace OAuthMyLabService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IOptions<OAuthSettings> _options;
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;

        public LoginController(
            IOptions<OAuthSettings> options,
            ILoginService loginService,
            ITokenService tokenService)
        {
            _options = options;
            _loginService = loginService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        async public Task<IActionResult> Post(Credentials credentials)
        {
            if (!ModelState.IsValid)
                return Unauthorized("Invalid model");

            var logged = await _loginService.LoginAsync(credentials);
            if (!logged)
                return Unauthorized("Invalid login");

            var token = await _tokenService.BuildTokenAsync(credentials, _options.Value);
            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized("Invalid token");

            return Ok(new
            {
                access_token = token,
                expiry_in_minutes = _options.Value.ExpiryDurationMinutes
            });
        }
    }
}