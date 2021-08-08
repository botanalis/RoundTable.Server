using System;
using System.Linq;
using RoundTable.Server.Data.Model;
using RoundTable.Server.Interfaces.Repositories;
using RoundTable.Server.Interfaces.Services;
using RoundTable.Server.Models;

namespace RoundTable.Server.Handlers.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        private readonly IUserAuthTokenRepository _userAuthTokenRepository;

        public UserService(
            IUserRepository userRepository, 
            IUserAuthTokenRepository userAuthTokenRepository)
        {
            this._userRepository = userRepository;
            this._userAuthTokenRepository = userAuthTokenRepository;
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

        public UserInfo GetIdentityVerification(string account, string password)
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
            
            return new UserInfo()
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }

        public void RemoveOldRefreshTokens(UserInfo user)
        {
            this._userAuthTokenRepository.DeleteById(user.Id);
        }

        public void RegisterRefreshTokens(UserInfo user, string refreshToken, DateTime expires, bool isRemoveOldToken = true)
        {
            var model = new UserAuthToken()
            {
                Id = user.Id,
                Token = refreshToken,
                Expires = expires,
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = ""
            };

            this._userAuthTokenRepository.Add(model, isRemoveOldToken);
        }

        public void UpdateRefreshTokens(UserInfo user, string refreshToken, DateTime expires)
        {

            var model = this._userAuthTokenRepository.GetById(user.Id);

            model.Token = refreshToken;
            model.Expires = expires;

            this._userAuthTokenRepository.Update(model);

        }

        public string GetUserRefreshTokenById(int id)
        {
            var model = this._userAuthTokenRepository.GetById(id);
            
            return model?.Token;
        }
    }
}