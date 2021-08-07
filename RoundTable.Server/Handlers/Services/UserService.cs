using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoundTable.Server.Data.Model;
using RoundTable.Server.Handlers.Repositories;
using RoundTable.Server.Handlers.Utils;
using RoundTable.Server.Interfaces.Repositories;
using RoundTable.Server.Interfaces.Services;
using RoundTable.Server.Interfaces.Utils;
using RoundTable.Server.Models;

namespace RoundTable.Server.Handlers.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        public User GetIdentityVerification(string account, string password)
        {
            var user = _userRepository
                .GetAll()
                .SingleOrDefault(x => 
                    x.Account == account && 
                    x.Password == password);

            if (user == null)
            {
                return null;
            }
            
            return user;
        }
    }
}