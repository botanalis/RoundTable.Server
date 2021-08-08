using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoundTable.Server.Handlers.Services;
using RoundTable.Server.Handlers.Utils;
using RoundTable.Server.Interfaces.Services;
using RoundTable.Server.Interfaces.Utils;
using RoundTable.Server.Models;

namespace RoundTable.Server.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtAuthOptions _options;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtAuthOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtOption jwtOption)
        {
            var token = context.Request.Headers["Authorization"]
                .FirstOrDefault()?
                .Split(" ")
                .Last();

            var jwtTokenInfo = jwtOption.ValidateJwtToken(token);

            if (jwtTokenInfo != null)
            {
                context.Items["User"] = userService.GetById(jwtTokenInfo.UserId);
                context.Items["RefreshToken"] = jwtTokenInfo.RefreshToken;
            }

            await _next(context);

        }

        
    }
}