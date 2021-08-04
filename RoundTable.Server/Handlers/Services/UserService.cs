using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoundTable.Server.Data.Model;
using RoundTable.Server.Handlers.Repositories;
using RoundTable.Server.Models;

namespace RoundTable.Server.Handlers.Services
{
    public class UserService : IUserService
    {

        private readonly JwtAuthOptions _options;

        private readonly IUserRepository _userRepository;

        public UserService(
            IOptions<JwtAuthOptions> options,
            IUserRepository userRepository)
        {
            _options = options.Value;
            _userRepository = userRepository;
        }

        public LoginReult Authenticate(LoginUser model)
        {
            var user = _userRepository
                .GetAll()
                .SingleOrDefault(x => 
                    x.Account == model.Account && 
                    x.Password == model.Password);

            if (user == null)
            {
                return null;
            }

            var token = this.GenerateJwtToken(user);

            return new LoginReult(user, token);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Secret);
            var tokenDescriptior = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.Id.ToString())}),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptior);
            return tokenHandler.WriteToken(token);
        }

        public UserInfo GetById(int id)
        {
            var user = this._userRepository.GetById(id);

            if (user == null)
            {
                return null;
            }

            return new UserInfo()
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }
    }
}