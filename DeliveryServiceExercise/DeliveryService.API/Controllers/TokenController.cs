using DeliveryService.API.Settings;
using DeliveryService.Common;
using DeliveryService.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DeliveryService.API.Controllers
{
    [Route("/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IUserManager _userManager;

        public TokenController(IOptions<AppSettings> appSettings,
            IUserManager userManager
            )
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult GetToken([FromBody] UserAuthenticationVM user)
        {
            if (ModelState.IsValid)
            {
                var result = _userManager.Authenticate(user.Login, user.Password);
                if(result.IsSuccessful)
                {
                    return new OkObjectResult(new { token = GenerateToken(result.User) });
                }
            }
            return new BadRequestResult();
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
                {
                  new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
                  new Claim(JwtRegisteredClaimNames.Jti, System.Guid.NewGuid().ToString()),
                };

            if(user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_appSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _appSettings.Issuer, // My app: issued by
                audience: _appSettings.Audience, // Client app: issued for
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(30), // Valid for 30 days
                signingCredentials: creds
            );

            var tokenEncoded = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenEncoded;
        }
    }
}