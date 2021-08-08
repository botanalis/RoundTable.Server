using System;
using Microsoft.AspNetCore.Http;
using RoundTable.Server.Data.Model;

namespace RoundTable.Server.Models
{
    public class AuthResult
    {

        /// <summary>
        /// 使用者
        /// </summary>
        public UserInfo UserInfo  { get; }

        /// <summary>
        /// Jwt Token
        /// </summary>
        public string JwtToken { get; }

        /// <summary>
        /// Refresh Token
        /// </summary>
        public string RefreshToken { get; }
        
        /// <summary>
        /// Cookies
        /// </summary>
        public CookieOptions CookieOptions { get; }
        
        
        public AuthResult(UserInfo user, string jwtToken, string refreshToken, DateTime expires)
        {
            this.UserInfo = user;

            this.JwtToken = jwtToken;

            this.RefreshToken = refreshToken;

            this.CookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = expires
            };
        }
        
    }
}