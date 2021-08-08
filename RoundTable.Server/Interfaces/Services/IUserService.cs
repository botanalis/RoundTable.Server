using System;
using RoundTable.Server.Data.Model;
using RoundTable.Server.Models;

namespace RoundTable.Server.Interfaces.Services
{
    public interface IUserService
    {
        
        /// <summary>
        /// 取得使用者,依據 User ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserInfo GetById(int id);

        /// <summary>
        /// 取得身分
        /// </summary>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        /// <returns></returns>
        UserInfo GetIdentityVerification(string account, string password);

        /// <summary>
        /// 移除 Old Refresh Token
        /// </summary>
        /// <param name="user">使用者</param>
        void RemoveOldRefreshTokens(UserInfo user);

        /// <summary>
        /// 註冊 Refresh Token
        /// </summary>
        /// <param name="user">使用者</param>
        /// <param name="refreshToken">Refresh Token</param>
        /// <param name="expires">有效期限</param>
        /// <param name="isRemoveOldToken">清除舊 Token</param>
        void RegisterRefreshTokens(UserInfo user, string refreshToken, DateTime expires, bool isRemoveOldToken);
        
        /// <summary>
        /// 更新 Refresh Token
        /// </summary>
        /// <param name="user">使用者</param>
        /// <param name="refreshToken">Refresh Token</param>
        /// <param name="expires">有效期限</param>
        void UpdateRefreshTokens(UserInfo user, string refreshToken, DateTime expires);
        
        /// <summary>
        /// 取得使用者 Refresh Token
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetUserRefreshTokenById(int id);
    }
}