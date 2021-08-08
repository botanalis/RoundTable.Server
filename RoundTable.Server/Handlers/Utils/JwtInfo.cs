using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoundTable.Server.Data.Model;
using RoundTable.Server.Interfaces.Utils;
using RoundTable.Server.Models;

namespace RoundTable.Server.Handlers.Utils
{
    public class JwtInfo : IJwtOption
    {
        private readonly JwtAuthOptions _options;

        public JwtInfo(IOptions<JwtAuthOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateJwtToken(UserInfo user, string refreshToken, DateTime? expires)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Secret);
            var tokenDescriptior = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim("id", user.Id.ToString()),
                        new Claim("refreshToken", refreshToken)
                    }),
                Expires = expires ?? DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptior);
            return tokenHandler.WriteToken(token);
        }

        public JwtTokenInfo ValidateJwtToken(string token)
        {
            if (token == null)
            {
                return null;
            }
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Secret);
            
            try
            {
                
                tokenHandler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken) validatedToken;

                return new JwtTokenInfo()
                {
                    UserId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value),
                    RefreshToken = jwtToken.Claims.First(x => x.Type == "refreshToken").Value
                };
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];

            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(randomBytes);
            }
            
            return Convert.ToBase64String(randomBytes);
        }
    }
}